namespace CinemaApp.Infrastructure.Data.Models.Enums
{
    public enum Genre
    {
        // в случай, че вече сме добавили филми в базата, след това вкараме като пропърти жанра, неговата стойност по дефолт излиза 0, затова е добре като първа стойност да е NotClassified
        NotClassified,
        Action,
        Comedy,
        Drama,
        Thriller,
        SciFi,
        Fantasy,
        Horror
    }
}
