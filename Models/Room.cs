namespace APBD_Task_7.Models;

public class Room
{
    public static List<Room> Rooms = new List<Room>();
    public int Id  { get; set; }
    public String Name  { get; set; }
    public String BuildingCode { get; set; }
    public int Capacity { get; set; }
    public int Floor  { get; set; }
    public bool HasProjector  { get; set; }

// Floor
 //       HasProjector
   // IsActive
}