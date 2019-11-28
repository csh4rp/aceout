CREATE TABLE User
(
	Id INT NOT NULL AUTO_INCREMENT,
	CreatedDate DATETIME(0) NOT NULL,
	ModifiedDate DATETIME(0) NOT NULL,
	UserName VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	PasswordHash VARCHAR(1000) NOT NULL,
	PhoneNumber VARCHAR(15) NULL,
	LockoutEndDate DATETIME(0) NULL,
	AccessFailedCount INT NOT NULL,
	ActivationToken VARCHAR(255) NULL,
	IsEmailConfirmed BIT NOT NULL,
	IsLockoutEnabled BIT NOT NULL,
	IsPhoneNumberConfirmed BIT NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY (Id),
	UNIQUE KEY IX_User_UserName (UserName),
	UNIQUE KEY IX_User_Email (Email)
	
);

CREATE TABLE Role
(
	Id INT NOT NULL AUTO_INCREMENT,
	Name VARCHAR(255) NOT NULL,
	CONSTRAINT PK_Role PRIMARY KEY (Id),
	UNIQUE KEY IX_Role_Name (Name)
);

CREATE TABLE UserRole
(
	UserId INT NOT NULL,
	RoleId INT NOT NULL,
	CONSTRAINT PK_UserRole PRIMARY KEY (UserId, RoleId),
	CONSTRAINT FK_UserRole_User FOREIGN KEY (UserId)
	REFERENCES User(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserRole_Role FOREIGN KEY (RoleId)
	REFERENCES Role(Id) ON DELETE CASCADE
);

CREATE TABLE RolePermission
(
	RoleId INT NOT NULL,
	Permission VARCHAR(255) NOT NULL,
	CONSTRAINT PK_RolePermission PRIMARY KEY(RoleId, Permission),
	CONSTRAINT FK_RolePermission_Role FOREIGN KEY(RoleId)
	REFERENCES Role(Id) ON DELETE CASCADE,
	UNIQUE KEY IX_RolePermission_Permission (RoleId, Permission)
);

CREATE TABLE PasswordPolicy
(
	Id INT NOT NULL AUTO_INCREMENT,
	MinLength INT NULL,
	MaxLength INT NULL,
	RequireSpecialCharacters BIT NOT NULL,
	RequireSmallAndBigLetters BIT NOT NULL,
	RequireNumbers BIT NOT NULL,
	CONSTRAINT PK_PasswordPolicy PRIMARY KEY (Id)
);

CREATE TABLE UserClaim
(
	Id INT NOT NULL AUTO_INCREMENT,
	UserId INT NOT NULL,
	Type VARCHAR(255) NOT NULL,
	Value VARCHAR(1000) NOT NULL,
	CONSTRAINT PK_UserClaim PRIMARY KEY(Id),
	CONSTRAINT FK_UserClaim_User FOREIGN KEY(UserId)
	REFERENCES User(Id)
);


INSERT INTO User
(
	CreatedDate,
	ModifiedDate,
	UserName,
	Email,
	FirstName,
	LastName,
	PasswordHash,
	IsEmailConfirmed,
	IsPhoneNumberConfirmed,
	IsLockoutEnabled,
	AccessFailedCount
)
VALUES
(
	curdate(),
	curdate(),
	'k.bieszcz',
	'bieszczyk2@gmail.com',
	'Kamil',
	'Bieszcz',
	'lYERWzrKUSanealOxgdmJPar83qpsb+lG3d/LjhYEXxsvzd/qH1LWnUZuP/9vYk8R/SAOBVs4RwbtlW/gt47Qs/IS4X/HebnKmih3yIr9VQXcCRCBbU3RmOijTj1qhHzse7ka+Hopra23e4TFDWG2VNcZxlcYbfaeTStK8JO8p+rwpMalH+OnmAgOUuwVXj7fPmYGeRrm9eCnUri26eljJJQf0XzS3me2oeSvhL3wE1iIiV/rxkeuoW1TSsCZCWZ',
	1,
	0,
	0,
	0
);

INSERT INTO Role
(
	Name
)
VALUES
(
	'Admin'
);

INSERT INTO UserRole
(
	UserId,
	RoleId
)
VALUES
(
	1,
	1
);

INSERT INTO RolePermission
(
	RoleId,
	Permission
)
VALUES
(
	1,
	'Administration'
);

CREATE TABLE MaterialCategory
(
	Id INT NOT NULL AUTO_INCREMENT,
	Language VARCHAR(2) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	CONSTRAINT PK_MaterialCategory PRIMARY KEY(Id),
	UNIQUE KEY IX_MaterialCategory_Name (Name)
);

CREATE TABLE Material
(
	Id INT NOT NULL AUTO_INCREMENT,
	Type INT NOT NULL,
	CategoryId INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Content TEXT NOT NULL,
	DataModel TEXT NULL,
	AnswerModel TEXT NULL,
	IsActive BIT NOT NULL,
	CONSTRAINT PK_Material PRIMARY KEY(Id),
	CONSTRAINT FK_aterial_MaterialCategory FOREIGN KEY(CategoryId)
	REFERENCES MaterialCategory(Id) ON DELETE CASCADE,
	UNIQUE KEY IX_Material_Name (Name)
);

CREATE TABLE CoursePath
(
	Id INT NOT NULL AUTO_INCREMENT,
	Language VARCHAR(2) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	PictureUrl VARCHAR(255) NULL,
	Description TEXT NULL,
	CONSTRAINT PK_CoursePath PRIMARY KEY(Id),
	UNIQUE KEY IX_CoursePath_Name (Name)
);

CREATE TABLE Course
(
	Id INT NOT NULL AUTO_INCREMENT,
	CoursePathId INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Description TEXT NULL,
	IsActive BIT NOT NULL,
	PictureUrl VARCHAR(255) NULL,
	RequireLessonOrder BIT NOT NULL,
	PassThreshold DECIMAL(5,4) NULL,
	CONSTRAINT PK_Course PRIMARY KEY(Id),
	CONSTRAINT FK_Course_CoursePath FOREIGN KEY(CoursePathId)
	REFERENCES CoursePath(Id) ON DELETE CASCADE,
	UNIQUE KEY IX_Course_Name (Name)
);

CREATE TABLE Lesson
(
	Id INT NOT NULL AUTO_INCREMENT,
	CourseId INT NOT NULL,
	Type INT NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Description TEXT NULL,
	Position INT NOT NULL,
	AttemptCount INT NULL,
	PassThreshold DECIMAL(5,4) NULL,
	IsActive BIT NOT NULL,
	AllowAnswerCheck BIT NOT NULL,
	AllowAnswerPreview BIT NOT NULL,
	CONSTRAINT PK_Lesson PRIMARY KEY(Id),
	CONSTRAINT FK_Lesson_Course FOREIGN KEY(CourseId)
	REFERENCES Course(Id) ON DELETE CASCADE,
	UNIQUE KEY IX_Lesson_Name (CourseId, Name)
);

CREATE TABLE Element
(
	Id INT NOT NULL AUTO_INCREMENT,
	LessonId INT NOT NULL,
	MaterialId INT NOT NULL,
	IsActive BIT NOT NULL,
	Position INT NOT NULL,
	Scale INT NOT NULL,
	CONSTRAINT PK_Element PRIMARY KEY(Id),
	CONSTRAINT FK_Element_Lesson FOREIGN KEY(LessonId)
	REFERENCES Lesson(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Element_Material FOREIGN KEY(MaterialId)
	REFERENCES Material(Id) ON DELETE CASCADE,
	UNIQUE KEY IX_Element_Lesson (LessonId, MaterialId)
);

CREATE TABLE `Group`
(
	Id INT NOT NULL AUTO_INCREMENT,
	Language VARCHAR(2) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	CONSTRAINT PK_Group PRIMARY KEY(Id),
	UNIQUE KEY IX_Group_Name(Name)
);

CREATE TABLE GroupUser
(
	UserId INT NOT NULL,
	GroupId INT NOT NULL,
	CONSTRAINT PK_GroupUser PRIMARY KEY (UserId, GroupId),
	CONSTRAINT FK_GroupUser_User FOREIGN KEY(UserId)
	REFERENCES User(Id) ON DELETE CASCADE,
	CONSTRAINT FK_GroupUser_Group FOREIGN KEY(GroupId)
	REFERENCES `Group`(Id) ON DELETE CASCADE
);

CREATE TABLE GroupCourse
(
	CourseId INT NOT NULL,
	GroupId INT NOT NULL,
	FromDate DATETIME(0) NULL,
	ToDate DATETIME(0) NULL,
	AttemptCount INT NULL,
	CONSTRAINT PK_GroupCourse PRIMARY KEY(CourseId, GroupId),
	CONSTRAINT FK_GroupCourse_Course FOREIGN KEY(CourseId)
	REFERENCES Course(Id) ON DELETE CASCADE,
	CONSTRAINT FK_GroupCourse_Group FOREIGN KEY(GroupId)
	REFERENCES `Group`(Id) ON DELETE CASCADE
);

CREATE TABLE UserCourse
(
	Id INT NOT NULL AUTO_INCREMENT,
	UserId INT NOT NULL,
	CourseId INT NOT NULL,
	StartedDate DATETIME(0) NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CompletedDate DATETIME(0) NULL,
	Attempt INT NOT NULL,
	IsPassed BIT NOT NULL,
	Result DECIMAL(5,4) NULL,
	CONSTRAINT PK_UserCourse PRIMARY KEY(Id),
	CONSTRAINT FK_UserCourse_Course FOREIGN KEY(CourseId)
	REFERENCES Course(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserCourse_User FOREIGN KEY(UserId)
	REFERENCES User(Id) ON DELETE CASCADE
);


CREATE TRIGGER TR_UserCourse_INSERT BEFORE INSERT ON UserCourse
FOR EACH ROW
SET NEW.Attempt =  
	CASE WHEN EXISTS (SELECT Id
	FROM UserCourse
	WHERE UserId = NEW.UserId AND CourseId = NEW.CourseId) THEN
		(SELECT MAX(Attempt) + 1
		FROM UserCourse
		WHERE UserId = NEW.UserId AND CourseId = NEW.CourseId)
	ELSE  1
END;

CREATE TABLE UserLesson
(
	Id INT NOT NULL AUTO_INCREMENT,
	UserId INT NOT NULL,
	LessonId INT NOT NULL,
	UserCourseId INT NOT NULL,
	StartedDate DATETIME(0) NOT NULL,
	CompletedDate DATETIME(0) NULL,
	Attempt INT NOT NULL,
	IsPassed BIT NOT NULL,
	Result DECIMAL(5,4) NULL,
	CONSTRAINT PK_UserLesson PRIMARY KEY(Id),
	CONSTRAINT FK_UserLesson_Lesson FOREIGN KEY(LessonId)
	REFERENCES Lesson(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserLesson_User FOREIGN KEY(UserId)
	REFERENCES User(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserLesson_UserCourse FOREIGN KEY(UserCourseId)
	REFERENCES UserCourse(Id) ON DELETE CASCADE
);

CREATE TRIGGER TR_UserLesson_INSERT BEFORE INSERT ON UserLesson
FOR EACH ROW
SET NEW.Attempt =  
	CASE WHEN EXISTS (SELECT Id
	FROM UserLesson
	WHERE UserId = NEW.UserId AND LessonId = NEW.LessonId) THEN
		(SELECT MAX(Attempt) + 1
		FROM UserLesson
		WHERE UserId = NEW.UserId AND LessonId = NEW.LessonId)
	ELSE  1
END;

CREATE TABLE UserElement
(
	ElementId INT NOT NULL,
	UserLessonId INT NOT NULL,
	UserAnswerModel VARCHAR(8000) NULL,
	Result DECIMAL(5,4) NULL,
	CONSTRAINT PK_UserElement PRIMARY KEY
	(
		ElementId,
		UserLessonId
	),
	CONSTRAINT FK_UserElement_Element FOREIGN KEY(ElementId)
	REFERENCES Element(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserElement_UserLesson FOREIGN KEY(UserLessonId)
	REFERENCES UserLesson(Id) ON DELETE CASCADE
);


CREATE VIEW LessonAccessInfo 
AS
SELECT
	gu.UserId as UserId,
	gc.GroupId AS GroupId,
	c.Id AS CourseId,
	l.Id AS LessonId,
	ul.Id AS UserLessonId,
	uc.Id AS UserCourseId,
	gc.FromDate AS GroupFromDate,
	gc.ToDate AS GroupToDate,
	c.RequireLessonOrder AS RequireLessonOrder,
	c.PassThreshold AS CoursePassThreshold,
	c.IsActive AS IsCourseActive,
	l.IsActive AS IsLessonActive,
	l.AttemptCount AS LessonAttemptCount,
	l.Name AS  LessonName,
	l.Type AS LessonType,
	l.Description AS LessonDescription,
	gc.AttemptCount AS GroupCourseAttemptCount,
	l.Position AS Position,
	l.AllowAnswerCheck AS AllowAnswerCheck,
	l.AllowAnswerPreview AS AllowAnswerPreview,
	l.PassThreshold  AS LessonPassThreshold,
	uc.StartedDate AS CourseStartedDate,
	uc.CompletedDate AS CourseCompletedDate,
	uc.Attempt AS CourseAttempt,
	uc.IsPassed AS IsCoursePassed,
	uc.Result AS CourseResult,
	ul.Attempt AS UserLessonAttempt,
	ul.StartedDate AS LessonStartedDate,
	ul.CompletedDate AS LessonCompletedDate,
	ul.IsPassed AS IsLessonPassed,
	ul.Result AS LessonResult
	FROM GroupUser gu
	INNER JOIN GroupCourse gc
	ON gc.GroupId = gu.GroupId
	INNER JOIN Course c
	ON gc.CourseId = c.Id
	INNER JOIN Lesson l
	ON l.CourseId = c.Id
	LEFT JOIN UserLesson ul
	ON l.Id = ul.LessonId AND gu.UserId = ul.UserId
	LEFT JOIN UserCourse uc
	ON gc.CourseId = uc.CourseId AND gu.UserId = uc.UserId
	WHERE (uc.Attempt IS NULL 
			OR
			uc.Attempt = (SELECT MAX(ucr.Attempt)
						  FROM UserCourse ucr
						  WHERE ucr.UserId = gu.UserId AND
						  ucr.CourseId = c.Id)
		   )
	 AND
		(ul.Attempt IS NULL 
		 OR
		 ul.Attempt = (SELECT MAX(ule.Attempt)
					   FROM UserLesson ule
				       WHERE ule.UserId = gu.UserId AND
                       ule.LessonId = l.Id)
		);


CREATE VIEW CourseAccessInfo 
AS
SELECT
	gu.UserId as UserId,
	gc.GroupId AS GroupId,
	c.Id AS CourseId,
	uc.Id AS UserCourseId,
	c.CoursePathId AS CoursePathId,
	c.Name AS CourseName,
	c.Description AS CourseDescription,
    c.PictureUrl AS PictureUrl,
	gc.FromDate AS GroupFromDate,
	gc.ToDate AS GroupToDate,
	c.IsActive AS IsCourseActive,
	gc.AttemptCount AS GroupCourseAttemptCount,
	uc.Attempt AS UserCourseAttempt,
	uc.Result AS UserCourseResult,
	uc.IsPassed AS IsPassed,
	uc.StartedDate AS StartedDate,
	uc.CompletedDate AS CompletedDate
	FROM GroupUser gu
	INNER JOIN GroupCourse gc
	ON gc.GroupId = gu.GroupId
	INNER JOIN Course c
	ON gc.CourseId = c.Id
	LEFT OUTER JOIN UserCourse uc
	ON gu.UserId = uc.UserId AND c.Id = uc.CourseId
    WHERE uc.Attempt IS NULL 
    OR
    uc.Attempt = (SELECT MAX(ucr.Attempt)
			     FROM UserCourse ucr
                 WHERE ucr.UserId = gu.UserId AND
                 ucr.CourseId = c.Id);



CREATE VIEW UserMaterialAnswer
AS
	SELECT 
	m.Name AS MaterialName,
	m.Content AS MaterialContent,
	m.DataModel AS MaterialDataModel,
	m.AnswerModel AS MaterialAnswerModel,
	m.Type AS MaterialType,
	e.Id AS ElementId,
	e.LessonId AS LessonId,
	e.Position AS ElementPosition,
	ul.Id AS UserLessonId,
	ue.UserAnswerModel AS UserAnswerModel,
	ul.UserId AS UserId
	FROM Element e
	INNER JOIN Material m
	ON e.MaterialId = m.Id
	INNER JOIN UserLesson ul
	ON e.LessonId = ul.LessonId
	LEFT OUTER JOIN UserElement ue
	ON e.Id = ue.ElementId AND ul.Id = ue.UserLessonId;


CREATE VIEW UserElementInfo
AS
	SELECT 
	c.Id AS CourseId,
	c.IsActive AS IsCourseActive,
	c.RequireLessonOrder AS RequireLessonOrder,
	l.Id AS LessonId,
	l.IsActive AS IsLessonActive,
	l.Position AS LessonPosition,
	l.AllowAnswerPreview AS AllowAnswerPreview,
	l.AllowAnswerCheck AS AllowAnswerCheck,
	ul.StartedDate AS LessonStartedDate,
	ul.CompletedDate AS LessonCompletedDate,
	ul.Id AS UserLessonId,
	ul.Attempt AS LessonAttempt,
	uc.Attempt As CourseAttempt,
	uc.StartedDate AS CourseStartedDate,
	uc.CompletedDate AS CourseCompletedDate,
	gc.FromDate AS FromDate,
	gc.ToDate AS ToDate,
	gu.UserId AS UserId,
	e.Id AS ElementId,
	e.IsActive AS IsElementActive,
	e.Position AS ElementPosition,
	e.Scale AS  ElementScale,
	m.IsActive AS IsMaterialActive,
	m.Type AS MaterialType,
	m.Name AS MaterialName,
	m.Content AS MaterialContent,
	m.DataModel AS MaterialDataModel,
	m.AnswerModel AS MaterialAnswerModel,
	ue.UserAnswerModel AS UserAnswerModel
	FROM Course c
	INNER JOIN GroupCourse gc
	ON c.Id = gc.CourseId
	INNER JOIN Lesson l
	ON l.CourseId = c.Id
	INNER JOIN Element e
	ON l.Id = e.LessonId
	INNER JOIN Material m
	ON e.MaterialId = m.Id
	INNER JOIN GroupUser gu
	ON gc.GroupId = gu.GroupId
	INNER JOIN UserCourse uc
	ON gu.UserId = uc.UserId AND c.Id = uc.CourseId AND gu.UserId = uc.UserId
	INNER JOIN UserLesson ul
	ON gu.UserId = ul.UserId AND uc.Id = ul.UserCourseId AND l.Id = ul.LessonId
	LEFT JOIN UserElement ue
	ON ul.Id = ue.UserLessonId AND ue.ElementId = e.Id
	WHERE (uc.Attempt IS NULL 
			OR
			uc.Attempt = (SELECT MAX(ucr.Attempt)
						  FROM UserCourse ucr
						  WHERE ucr.UserId = gu.UserId AND
						  ucr.CourseId = c.Id)
		   )
	 AND
		(ul.Attempt IS NULL 
		 OR
		 ul.Attempt = (SELECT MAX(ule.Attempt)
					   FROM UserLesson ule
				       WHERE ule.UserId = gu.UserId AND
                       ule.LessonId = l.Id)
		);
	