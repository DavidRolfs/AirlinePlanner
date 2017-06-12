DataBase: airline_planner

tables:

airport:
  id:
  name:

flights:
  id:
  arrival city:
  departure time:

airport_flights:
    id:
    airport_id:
    flight_id



      sqlcmd -S "(localdb)\mssqllocaldb"
    > create database airline_planner;
    > go
    > use airline_planner
    > go
    hanged database context to 'airline_planner'.
    > create table airport(id INT IDENTITY (1,1), name varchar(255));
    > go
    > create table flights(id INT IDENTITY (1,1), arrival_city varchar(255), time varchar(255));
    > go
    > create table airport_flights(id INT IDENTITY (1,1), airport_id INT, flight_id INT);
    > go
    > quit
