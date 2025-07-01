use EventDb

-- INSERT PROCEDURES

CREATE PROCEDURE InsertUserInfo
    @EmailId VARCHAR(50),
    @UserName VARCHAR(50),
    @Role VARCHAR(20),
    @Password VARCHAR(20)
AS
BEGIN
    INSERT INTO UserInfo (EmailId, UserName, Role, Password)
    VALUES (@EmailId, @UserName, @Role, @Password)
END


CREATE PROCEDURE InsertEventDetails
    @EventId INT,
    @EventName VARCHAR(50),
    @EventCategory VARCHAR(50),
    @EventDate DATETIME,
    @Description VARCHAR(50),
    @Status VARCHAR(15)
AS
BEGIN
    INSERT INTO EventDetails (EventId, EventName, EventCategory, EventDate, Description, Status)
    VALUES (@EventId, @EventName, @EventCategory, @EventDate, @Description, @Status)
END


CREATE PROCEDURE InsertSpeakersDetails
    @SpeakerId INT,
    @SpeakerName VARCHAR(50)
AS
BEGIN
    INSERT INTO SpeakersDetails (SpeakerId, SpeakerName)
    VALUES (@SpeakerId, @SpeakerName)
END


CREATE PROCEDURE InsertSessionInfo
    @SessionId INT,
    @EventId INT,
    @SessionTitle VARCHAR(50),
    @SpeakerId INT,
    @Description VARCHAR(50),
    @SessionStart DATETIME,
    @SessionEnd DATETIME,
    @SessionUrl VARCHAR(50)
AS
BEGIN
    INSERT INTO SessionInfo (SessionId, EventId, SessionTitle, SpeakerId, Description, SessionStart, SessionEnd, SessionUrl)
    VALUES (@SessionId, @EventId, @SessionTitle, @SpeakerId, @Description, @SessionStart, @SessionEnd, @SessionUrl)
END


CREATE PROCEDURE InsertParticipantEventDetails
    @Id INT,
    @ParticipantEmailId VARCHAR(50),
    @EventId INT,
    @SessionId INT,
    @IsAttended BIT
AS
BEGIN
    INSERT INTO ParticipantEventDetails (Id, ParticipantEmailId, EventId, SessionId, IsAttended)
    VALUES (@Id, @ParticipantEmailId, @EventId, @SessionId, @IsAttended)
END


-- UPDATE PROCEDURES

CREATE PROCEDURE UpdateUserInfo
    @EmailId VARCHAR(50),
    @UserName VARCHAR(50),
    @Role VARCHAR(20),
    @Password VARCHAR(20)
AS
BEGIN
    UPDATE UserInfo SET UserName = @UserName, Role = @Role, Password = @Password
    WHERE EmailId = @EmailId
END


CREATE PROCEDURE UpdateEventDetails
    @EventId INT,
    @EventName VARCHAR(50),
    @EventCategory VARCHAR(50),
    @EventDate DATETIME,
    @Description VARCHAR(50),
    @Status VARCHAR(15)
AS
BEGIN
    UPDATE EventDetails
    SET EventName = @EventName, EventCategory = @EventCategory, EventDate = @EventDate, Description = @Description, Status = @Status
    WHERE EventId = @EventId
END


CREATE PROCEDURE UpdateSpeakersDetails
    @SpeakerId INT,
    @SpeakerName VARCHAR(50)
AS
BEGIN
    UPDATE SpeakersDetails SET SpeakerName = @SpeakerName WHERE SpeakerId = @SpeakerId
END


CREATE PROCEDURE UpdateSessionInfo
    @SessionId INT,
    @EventId INT,
    @SessionTitle VARCHAR(50),
    @SpeakerId INT,
    @Description VARCHAR(50),
    @SessionStart DATETIME,
    @SessionEnd DATETIME,
    @SessionUrl VARCHAR(50)
AS
BEGIN
    UPDATE SessionInfo
    SET EventId = @EventId, SessionTitle = @SessionTitle, SpeakerId = @SpeakerId,
        Description = @Description, SessionStart = @SessionStart, SessionEnd = @SessionEnd, SessionUrl = @SessionUrl
    WHERE SessionId = @SessionId
END


CREATE PROCEDURE UpdateParticipantEventDetails
    @Id INT,
    @ParticipantEmailId VARCHAR(50),
    @EventId INT,
    @SessionId INT,
    @IsAttended BIT
AS
BEGIN
    UPDATE ParticipantEventDetails
    SET ParticipantEmailId = @ParticipantEmailId, EventId = @EventId, SessionId = @SessionId, IsAttended = @IsAttended
    WHERE Id = @Id
END


-- DELETE PROCEDURES

CREATE PROCEDURE DeleteUserInfo
    @EmailId VARCHAR(50)
AS
BEGIN
    DELETE FROM UserInfo WHERE EmailId = @EmailId
END


CREATE PROCEDURE DeleteEventDetails
    @EventId INT
AS
BEGIN
    DELETE FROM EventDetails WHERE EventId = @EventId
END


CREATE PROCEDURE DeleteSpeakersDetails
    @SpeakerId INT
AS
BEGIN
    DELETE FROM SpeakersDetails WHERE SpeakerId = @SpeakerId
END


CREATE PROCEDURE DeleteSessionInfo
    @SessionId INT
AS
BEGIN
    DELETE FROM SessionInfo WHERE SessionId = @SessionId
END


CREATE PROCEDURE DeleteParticipantEventDetails
    @Id INT
AS
BEGIN
    DELETE FROM ParticipantEventDetails WHERE Id = @Id
END


-- VIEWS

CREATE VIEW View_SessionDetails AS
SELECT s.SessionId, s.SessionTitle, s.Description, s.SessionStart, s.SessionEnd, s.SessionUrl, e.EventName
FROM SessionInfo s
JOIN EventDetails e ON s.EventId = e.EventId


CREATE VIEW View_SpeakerDetails AS
SELECT s.SessionId, s.SessionTitle, sp.SpeakerName
FROM SessionInfo s
JOIN SpeakersDetails sp ON s.SpeakerId = sp.SpeakerId


CREATE VIEW View_EventFullDetails AS
SELECT e.EventId, e.EventName, e.EventCategory, e.EventDate, e.Description AS EventDescription, e.Status,
       si.SessionId, si.SessionTitle, si.Description AS SessionDescription, si.SessionStart, si.SessionEnd,
       sp.SpeakerName, u.EmailId AS ParticipantEmail, u.UserName, ped.IsAttended
FROM EventDetails e
JOIN SessionInfo si ON e.EventId = si.EventId
JOIN SpeakersDetails sp ON si.SpeakerId = sp.SpeakerId
LEFT JOIN ParticipantEventDetails ped ON e.EventId = ped.EventId AND si.SessionId = ped.SessionId
LEFT JOIN UserInfo u ON ped.ParticipantEmailId = u.EmailId


-- INDEXES

CREATE NONCLUSTERED INDEX IX_UserInfo_EmailId ON UserInfo(EmailId);

CREATE NONCLUSTERED INDEX IX_EventDetails_EventId ON EventDetails(EventId);

CREATE NONCLUSTERED INDEX IX_SessionInfo_SessionId ON SessionInfo(SessionId);

CREATE NONCLUSTERED INDEX IX_SessionInfo_EventId ON SessionInfo(EventId);

CREATE NONCLUSTERED INDEX IX_ParticipantEventDetails_Email ON ParticipantEventDetails(ParticipantEmailId);






