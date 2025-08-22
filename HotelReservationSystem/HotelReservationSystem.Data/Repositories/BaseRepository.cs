namespace HotelReservationSystem.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly string connectionString;
        public BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
