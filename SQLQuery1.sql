CREATE TABLE [dbo].[Bookings] (
[BookingID] int IDENTITY (1,1) NOT NULL ,
[SessionID] int NOT NULL ,
[UserID] nvarchar (max) NOT NULL ,
PRIMARY KEY (BookingID),
FOREIGN KEY (SessionID) REFERENCES Sessions(SessionID)
);
GO