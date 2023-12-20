using RestaurantReservatie.BL.Exceptions;

namespace RestaurantReservatie.BL.Models; 

public class Table {
  
        public int TableId { get;  set; }
        public int Chairs { get;  set; }
        public int RestaurantID { get;  set; }
        public int TableNumber { get;  set; }
        
        public bool isReserved{ get; set; }
        
        
        public Table(int chairs, int tableNumber, int restaurantId)
        {
            SetChairs(chairs);
            SetTableNumber(tableNumber);
            RestaurantID = restaurantId;
        }

        public Table(int tableId, int chairs, int tableNumber, int restaurantId) : this(chairs, tableNumber, restaurantId)
        {
            SetId(tableId);
        }

        public Table()
        {
        }

        public void SetChairs(int chairs)
        {
            if (chairs <= 0) throw new TableException("Aantal stoelen moet groter zijn dan 0");
            Chairs = chairs;
        }

        public void SetId(int tableId)
        {
            if (tableId <= 0) throw new TableException("Id moet groter zijn dan 0");
            TableId = tableId;
        }

        public void SetTableNumber(int tableNumber)
        {

            if (TableNumber < 0) throw new TableException("Tafelnummer moet groter zijn dan 0");
            TableNumber = tableNumber;
        }
}

