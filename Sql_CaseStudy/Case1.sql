create database EventDb

use EventDb

create table UserInfo(
EmailId varchar(50) primary key,
UserName varchar(50) not null check(len(UserName) between 1 and 50),
Role varchar(20) not null check(Role in('Admin','Participant')),
Password varchar(20) not null check(len(Password) between 6 and 20)
)

create table EventDetails(
EventId int primary key,
EventName varchar(50) not null,
EventCategory varchar(50) not null,
EventDate datetime not null,
Description varchar(50),
Status varchar(15) check(Status in ('Active','In-Active'))
)

create table SpeakersDetails(
SpeakerId int primary key,
SpeakerName varchar(50) not null
)

Create table SessionInfo(
SessionId int primary key,
EventId int not null,
SessionTitle varchar(50) not null,
SpeakerId int not null,
Description varchar(50),
SessionStart datetime not null,
SessionEnd datetime not null,
SessionUrl varchar(50),
foreign key(EventId) references EventDetails(EventId) on delete cascade,
foreign key(SpeakerId) references SpeakersDetails(SpeakerId)
)

create table ParticipantEventDetails(
Id int primary key,
ParticipantEmailId varchar(50) not null references UserInfo(EmailId),
EventId int not null references EventDetails(EventId) on delete cascade,
SessionId int not null references SessionInfo(SessionId),
IsAttended bit check(IsAttended in(0,1))
)
