using APBD_Task_7.Models;

namespace APBD_Task_7;

public static class Data
{
    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "Alpha", BuildingCode = "A", Floor = 1, Capacity = 10, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Beta", BuildingCode = "A", Floor = 2, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "Gamma", BuildingCode = "B", Floor = 1, Capacity = 5, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "Delta", BuildingCode = "B", Floor = 3, Capacity = 50, HasProjector = true, IsActive = false }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation { 
            Id = 1, RoomId = 1, OrganizerName = "Anna Kowalska", Topic = "Wstęp do C#", 
            Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), 
            Status = "confirmed" 
        },
        new Reservation { 
            Id = 2, RoomId = 2, OrganizerName = "Jan Nowak", Topic = "Warsztaty SQL", 
            Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), 
            Status = "confirmed" 
        },
        new Reservation { 
            Id = 3, RoomId = 2, OrganizerName = "Marek Ryś", Topic = "Code Review", 
            Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(15, 0, 0), 
            Status = "planned" 
        },
        new Reservation { 
            Id = 4, RoomId = 4, OrganizerName = "Zofia Web", Topic = "UI/UX Design", 
            Date = DateTime.Parse("2026-05-11"), StartTime = new TimeSpan(09, 30, 0), EndTime = new TimeSpan(11, 0, 0), 
            Status = "confirmed" 
        },
        new Reservation { 
            Id = 5, RoomId = 1, OrganizerName = "Piotr Programista", Topic = "Spotkanie Zarządu", 
            Date = DateTime.Parse("2026-05-12"), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0), 
            Status = "cancelled" 
        },
        new Reservation { 
            Id = 6, RoomId = 3, OrganizerName = "Ewa HR", Topic = "Rekrutacja", 
            Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0), 
            Status = "confirmed" 
        }    };
}