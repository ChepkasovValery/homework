using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public int Width => _width;
        public int Height => _height;
        public int Count => _items.Count;

        private Item[,] _grid;
        private Dictionary<Item, Vector2Int> _items = new Dictionary<Item, Vector2Int>();
        private int _width;
        private int _height;

        public Inventory(in int width, in int height)
        {
            if (width < 0 || height < 0 || width == 0 && height == 0)
                throw new ArgumentException();

            _width = width;
            _height = height;

            CreateGrid(width, height);
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            CreateInventoryWithItemsAndPositions(items);
        }
        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            CreateInventoryWithItems(items);
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            CreateInventoryWithItemsAndPositions(items);
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            CreateInventoryWithItems(items);
        }
        
        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (!ValidateItem(in item))
                return false;

            return CanAddItemOnPosition(position.x, position.y, item.Size.x, item.Size.y);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            if (item == null)
                return false;

            if (Contains(item))
                return false;

            return CanAddItemOnPosition(posX, posY, item.Size.x, item.Size.y);
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if (!ValidateItem(in item))
                return false;

            return AddItem(in item, position.x, position.y);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            if (CanAddItem(item, posX, posY))
            {
                FillGridArea(posX, posY, item);

                Vector2Int pos = new Vector2Int(posX, posY);

                _items.Add(item, pos);
                
                OnAdded?.Invoke(item, pos);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (!ValidateItem(in item))
                return false;

            return FindFreePosition(item.Size.x, item.Size.y, out Vector2Int v);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (!ValidateItem(in item))
                return false;

            if (FindFreePosition(in item, out Vector2Int pos))
            {
                return AddItem(in item, in pos);
            }

            return false;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Item item, out Vector2Int freePosition) => FindFreePosition(item.Size.x, item.Size.y, out freePosition);

        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (size.x <= 0 || size.y <= 0)
            {
                throw new ArgumentException();
            }

            if (size.x > _width || size.y > _height)
            {
                freePosition = Vector2Int.zero;
                return false;
            }

            return FindFreePosition(size.x, size.y, out freePosition);
        }

        public bool FindFreePosition(in int sizeX, int sizeY, out Vector2Int freePosition)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (CanAddItemOnPosition(x, y, sizeX, sizeY))
                    {
                        freePosition = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            freePosition = new Vector2Int(0, 0);
            return false;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            if (item == null)
                return false;
            
            return _items.ContainsKey(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position) => !IsFree(position);

        public bool IsOccupied(in int x, in int y) => !IsFree(x, y);

        /// <summary>
        /// Checks if the a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position) => _grid[position.x, position.y] == null;

        public bool IsFree(in int x, in int y) => IsFree(new Vector2Int(x, y));

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            Vector2Int pos = _items[item];
            
            ClearGridArea(pos.x, pos.y, item.Size.x, item.Size.y);

            _items.Remove(item);
                
            return true;
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = Vector2Int.zero;

            if (item == null)
                return false;
            
            if(_items.TryGetValue(item, out Vector2Int itemPos))
            {
                position = itemPos;
                
                RemoveItem(item);
                
                OnRemoved?.Invoke(item, itemPos);

                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position) => GetItem(position.x, position.y);

        public Item GetItem(in int x, in int y)
        {
            if (x < 0 || y < 0 || x >= _width || y >= _height)
                throw new IndexOutOfRangeException();

            if (TryGetItem(new Vector2Int(x, y), out Item item))
            {
                return item;
            }
            else
            {
                if (item == null)
                    throw new NullReferenceException();

                return null;
            }
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            if (position.x <= 0 || position.y <= 0 || position.x >= _width || position.y >= _height)
            {
                item = null;

                return false;
            }
            
            item = _grid[position.x, position.y];
            
            if (_grid[position.x, position.y] != null)
            {
                return true;
            }

            return false;
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                item = null;
                return false;
            }

            return TryGetItem(new Vector2Int(x, y), out item);
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            if (_items.TryGetValue(item, out Vector2Int itemPosition))
            {
                Vector2Int[] positions = new Vector2Int[item.Size.x * item.Size.y];
                int index = 0;
                
                for (int x = itemPosition.x; x < itemPosition.x + item.Size.x; x++)
                {
                    for (int y = itemPosition.y; y < itemPosition.y + item.Size.y; y++)
                    {
                        positions[index++] = new Vector2Int(x, y);
                    }
                }

                return positions;
            }

            throw new KeyNotFoundException();
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (!Contains(item))
                return false;

            positions = GetPositions(in item);

            if (positions != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (_items.Count > 0)
            {
                ClearGrid();

                _items.Clear();

                OnCleared?.Invoke();
            }
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;

            foreach (Item item in _items.Keys)
            {
                if (item.Name == name)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Moves a specified item at target position if exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int position)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (!_items.ContainsKey(item))
                return false;
            
            if (CanAddItemOnPositionWithoutSelfIntersect(position.x, position.y, item))
            {
                Vector2Int currentItemPos = _items[item];
                
                ClearGridArea(currentItemPos.x, currentItemPos.y, item.Size.x, item.Size.y);
                
                FillGridArea(position.x, position.y, item);

                _items[item] = position;
                
                OnMoved?.Invoke(item, position);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Reorganizes a inventory space so that the free area is uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            ClearGrid();
            
            var sortedBySize = from item in _items.Keys orderby item.Size.x * item.Size.y descending select item;

            foreach (Item item in sortedBySize)
            {
                FindFreePosition(item, out Vector2Int pos);
                
                FillGridArea(pos.x, pos.y, item);

                _items[item] = pos;
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix) => Array.Copy(_grid, matrix, matrix.Length);

        public IEnumerator<Item> GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void CreateGrid(int width, int height)
        {
            _grid = new Item[width, height];
        }
        
        private void ClearGrid() => ClearGridArea(0, 0, _height, _width);

        private bool ValidateItem(in Item item)
        {
            if (item == null)
                return false;
            
            CheckSizeOutOfBounds(item.Size.x, item.Size.y);

            if (IsItemWithNameExists(item))
                return false;
            
            return true;
        }

        private void CheckSizeOutOfBounds(int x, int y)
        {
            if (x <= 0 || y <= 0 || x > _width || y > _height)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsItemWithNameExists(in Item item)
        {
            foreach (KeyValuePair<Item,Vector2Int> pair in _items)
            {
                if (pair.Key.Name == item.Name)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool CanAddItemOnPosition(int posX, int posY, int sizeX, int sizeY)
        {
            if (posX < 0 || posX > _width || posY < 0 || posY > _height 
                || posX + sizeX > _width || posY + sizeY > _height)
                return false;

            for (int x = posX; x < posX + sizeX; x++)
            {
                for (int y = posY; y < posY + sizeY; y++)
                {
                    if (_grid[x, y] != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        private bool CanAddItemOnPositionWithoutSelfIntersect(int posX, int posY, Item item)
        {
            if (posX < 0 || posX > _width || posY < 0 || posY > _height 
                || posX + item.Size.x > _width || posY + item.Size.y > _height)
                return false;

            for (int x = posX; x < posX + item.Size.x; x++)
            {
                for (int y = posY; y < posY + item.Size.y; y++)
                {
                    if (_grid[x, y] != null && _grid[x, y] != item)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        private void CreateInventoryWithItems(IEnumerable<Item> items)
        {
            if (items == null)
                throw new ArgumentException();

            foreach (Item item in items)
            {
                AddItem(in item);
            }
        }

        private void CreateInventoryWithItemsAndPositions(IEnumerable<KeyValuePair<Item, Vector2Int>> items)
        {
            if (items == null)
                throw new ArgumentException();

            foreach (KeyValuePair<Item, Vector2Int> pair in items)
            {
                AddItem(pair.Key, pair.Value.x, pair.Value.y);
            }
        }
        
        private void ClearGridArea(int posX, int posY, int sizeX, int sizeY)
        {
            for (int x = posX; x < posX + sizeX; x++)
            {
                for (int y = posY; y < posY + sizeY; y++)
                {
                    _grid[x, y] = null;
                }
            }
        }
        
        private void FillGridArea(int posX, int posY, Item item)
        {
            for (int x = posX; x < posX + item.Size.x; x++)
            {
                for (int y = posY; y < posY + item.Size.y; y++)
                {
                    _grid[x, y] = item;
                }
            }
        }
    }
}