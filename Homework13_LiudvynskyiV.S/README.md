# Project Title
Cinema Network Database

## Description
Database contains tables with user and cinema halls, provides an ability to booking tickets with specifide showtime and seat.

## Database structure

### Entities
* Showtimes.
* Users.
* TicketHistories.
* Tickets.
* Cinemas.
* Halls.
* Seats.
* Films.

### Connection tables
* Showtimes (ShowtimeId, TimeOfSession).
* Users (UserId, FirstName, SecondName).
* TicketHistories (TicketHistoryId, UserId).
* Tickets (TicketId, Price, SeatId, FilmId, TicketHistoryId).
* Cinemas (CinemaId, CinemaName, Adress).
* Halls (HallId, HallNumber, CinemaId).
* Seats (SeatId, SeatNumber, HallId, IsAvailable).
* Films (FilmId, Title, HallId, ShowtimeId).

## Decisions
* I guess that tables can be simplier and so work on readable and understable database is important.

## Authors
Vitalii Liudvynskyi