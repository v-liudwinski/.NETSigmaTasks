CREATE DATABASE "CinemaNetwork"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1251'
    LC_CTYPE = 'English_United States.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

CREATE TABLE _user (
    UserId int GENERATED ALWAYS AS IDENTITY,
    FirstName varchar(50) NOT NULL,
    SecondName varchar(50) NOT NULL,
    PRIMARY KEY(UserId)
);

CREATE TABLE ticketHistory (
    TicketHistoryId int GENERATED ALWAYS AS IDENTITY,
  UserId int,
    PRIMARY KEY(TicketHistoryId),
    FOREIGN KEY (UserId) REFERENCES _user(UserId)
);

CREATE TABLE cinema (
    CinemaId int GENERATED ALWAYS AS IDENTITY,
    CinemaName varchar(50) NOT NULL,
    Adress varchar(256) NOT NULL,
    PRIMARY KEY(CinemaId)
);

CREATE TABLE hall (
    HallId int GENERATED ALWAYS AS IDENTITY,
    HallNumber int,
    CinemaId int,
    PRIMARY KEY(HallId),
    FOREIGN KEY (CinemaId) REFERENCES cinema(CinemaId)
);

CREATE TABLE seat (
    SeatId int GENERATED ALWAYS AS IDENTITY,
    SeatNumber int,
    HallId int,
    IsAvailable bool,
    PRIMARY KEY(SeatId),
    FOREIGN KEY (HallId) REFERENCES hall(HallId)
);

CREATE TABLE showtime (
    ShowtimeId int GENERATED ALWAYS AS IDENTITY,
    TimeOfSession timestamp NOT NULL,
    PRIMARY KEY(ShowtimeId)
);

CREATE TABLE film (
    FilmId int GENERATED ALWAYS AS IDENTITY,
    Title varchar(100),
    HallId int,
    ShowtimeId int,
    PRIMARY KEY(FilmId),
    FOREIGN KEY (HallId) REFERENCES hall(HallId),
    FOREIGN KEY (ShowtimeId) REFERENCES showtime(ShowtimeId)
);

CREATE TABLE ticket (
    TicketId int GENERATED ALWAYS AS IDENTITY,
    Price decimal NOT NULL,
    SeatId int,
    FilmId int,
    TicketHistoryId int,
    PRIMARY KEY(TicketId),
    FOREIGN KEY (SeatId) REFERENCES seat(SeatId),
    FOREIGN KEY (FilmId) REFERENCES film(FilmId),
  FOREIGN KEY (TicketHistoryId) REFERENCES ticketHistory(TicketHistoryId)
);

INSERT INTO _user (firstname, secondname) VALUES
('Otto', 'Von Bismark'),
('Brad', 'Pitt'),
('Angelina', 'Jolie'),
('Charlie', 'Hunnam'),
('Johny', 'Depp'),
('Robert', 'Dawny Junior');

INSERT INTO tickethistory (userid) VALUES
(1),
(2),
(3),
(4),
(5),
(6);

INSERT INTO showtime (timeofsession) VALUES
('2023-01-14 8:00'),
('2023-01-14 12:00'),
('2023-01-14 16:00'),
('2023-01-15 8:00'),
('2023-01-15 12:00'),
('2023-01-15 16:00'),
('2023-01-16 8:00'),
('2023-01-16 12:00'),
('2023-01-16 16:00'),
('2023-01-17 8:00'),
('2023-01-17 12:00'),
('2023-01-17 16:00');

INSERT INTO cinema (cinemaname, adress) VALUES
('Kyiv', 'Mykhaila Grushevskogo, 57'),
('Wizoria', 'Geroyiv Ukrayiny, 22');

INSERT INTO hall (hallnumber, cinemaid) VALUES
(1, 1),
(2, 1),
(1, 2),
(2, 2),
(3, 2);

INSERT INTO seat (seatnumber, hallid, isavailable) VALUES
(1, 1, true),
(2, 1, true),
(3, 1, true),
(4, 1, true),
(5, 1, true),
(6, 1, true),
(7, 1, true),
(8, 1, true),
(9, 1, true),
(10, 1, true),
(1, 2, true),
(2, 2, true),
(3, 2, true),
(4, 2, true),
(5, 2, true),
(6, 2, true),
(7, 2, true),
(8, 2, true),
(9, 2, true),
(10, 2, true),
(1, 3, true),
(2, 3, true),
(3, 3, true),
(4, 3, true),
(5, 3, true),
(6, 3, true),
(7, 3, true),
(8, 3, true),
(9, 3, true),
(10, 3, true),
(1, 4, true),
(2, 4, true),
(3, 4, true),
(4, 4, true),
(5, 4, true),
(6, 4, true),
(7, 4, true),
(8, 4, true),
(9, 4, true),
(10, 4, true),
(1, 5, true),
(2, 5, true),
(3, 5, true),
(4, 5, true),
(5, 5, true),
(6, 5, true),
(7, 5, true),
(8, 5, true),
(9, 5, true),
(10, 5, true);


INSERT INTO film (title, hallid, showtimeid) VALUES
('Gentlemen', 1, 1),
('Gentlemen', 1, 4),
('Gentlemen', 1, 7),
('Gentlemen', 1, 10),
('Ironman', 2, 1),
('Ironman', 2, 4),
('Ironman', 2, 7),
('Ironman', 2, 10);

INSERT INTO ticket (price, seatid, filmid, tickethistoryid) VALUES
(5, 1, 1, 1),
(5, 2, 1, 1),
(5, 3, 1, 2),
(5, 4, 1, 2),
(5, 5, 1, 3),
(5, 6, 1, 3),
(5, 7, 1, 4),
(5, 8, 1, 4),
(5, 9, 1, 4),
(7, 10, 1, 5),
(7, 11, 2, 5),
(7, 12, 2, 5),
(7, 13, 2, 6),
(7, 14, 2, 6),
(7, 15, 2, 6),
(7, 16, 2, 1),
(7, 17, 2, 1),
(7, 18, 2, 1),
(7, 19, 2, 3),
(7, 20, 2, 3);

SELECT (timeofsession, title) FROM ticket
JOIN film f on f.filmid = ticket.filmid
JOIN showtime s on s.showtimeid = f.showtimeid
WHERE EXTRACT(week FROM timeofsession) > 1;

SELECT (seatnumber, title) FROM ticket
JOIN film f on f.filmid = ticket.filmid
JOIN seat s on s.seatid = ticket.seatid
WHERE isavailable is true;

SELECT (seatnumber) FROM ticket
JOIN seat s on s.seatid = ticket.seatid
WHERE isavailable is true;

SELECT (title, count(price))FROM ticket
JOIN film f on f.filmid = ticket.filmid
GROUP BY title
ORDER BY count(price) DESC;

SELECT (firstname, secondname) FROM ticket
JOIN tickethistory t on t.tickethistoryid = ticket.tickethistoryid
JOIN _user u on u.userid = t.userid
JOIN film f on f.filmid = ticket.filmid
JOIN showtime s on s.showtimeid = f.showtimeid
WHERE EXTRACT(week FROM timeofsession) > 1
GROUP BY firstname, secondname
ORDER BY count(price) DESC LIMIT 3;

SELECT (hallid) FROM ticket
JOIN seat s on s.seatid = ticket.seatid
JOIN film f on ticket.filmid = f.filmid
JOIN showtime s2 on s2.showtimeid = f.showtimeid
WHERE EXTRACT(week FROM timeofsession) > 1
GROUP BY hallid
ORDER BY count(price) ASC;

SELECT DISTINCT t1.filmid, th.userid, th2.userid
FROM ticket t1
JOIN ticket t2 on t1.filmid = t2.filmid
JOIN film f on f.filmid = t1.filmid
JOIN showtime s on s.showtimeid = f.showtimeid
JOIN tickethistory th on t1.tickethistoryid = th.tickethistoryid
JOIN tickethistory th2 on t2.tickethistoryid = th2.tickethistoryid
where th.userid != th2.userid
AND t1.filmid = t2.filmid
ORDER BY t1.filmid ASC;