
\connect meeting_rooms

CREATE TABLE meeting_room
(
 id serial PRIMARY KEY,
 name VARCHAR (50) NULL,
 description VARCHAR (100) NULL,
 location GEOMETRY NULL,
 total_seats INTEGER NULL DEFAULT 0
);

ALTER TABLE meeting_room OWNER TO aspnetreact;

Insert into meeting_room(name ,description, location, total_seats) values( 'Meeting room name 1','Description 1', ST_GeomFromText('POINT(51.590722643120145 7.020263671875)', 4326), 5);
Insert into meeting_room(name, description, location, total_seats) values( 'Meeting room name 2','Description 2', ST_GeomFromText('POINT(51.2206474303833 7.646484374999999)', 4326), 4);
Insert into meeting_room(name, description, location, total_seats) values( 'Meeting room name 3','Description 3', ST_GeomFromText('POINT(50.548344490674786 5.328369140625)', 4326), 3);
Insert into meeting_room(name, description, location, total_seats) values( 'Meeting room name 4','Description 4', ST_GeomFromText('POINT(51.034485632974125 5.064697265625)', 4326), 2);
Insert into meeting_room(name, description, location, total_seats) values( 'Meeting room name 5','Description 5', ST_GeomFromText('POINT(51.55658218576253 5.44921875)', 4326), 1);