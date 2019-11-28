CREATE VIEW CourseReport
AS
	SELECT 
	u.Id AS UserId,
	u.UserName AS UserName,
	u.FirstName AS FirstName,
	u.LastName AS LastName,
	u.Email AS Email,
	uc.Id AS UserCourseId,
	uc.CourseId AS CourseId,
	uc.StartedDate AS StartedDate,
	uc.CompletedDate AS CompletedDate,
	uc.IsPassed AS IsPassed,
	uc.Result AS Result,
	uc.Attempt AS Attempt
	FROM User u
	JOIN UserCourse uc
	ON u.Id = uc.UserId;


CREATE VIEW LessonReport
AS
	SELECT
	u.Id AS UserId,
	u.UserName AS UserName,
	u.FirstName AS FirstName,
	u.LastName AS LastName,
	u.Email AS Email,
	ul.Id AS UserLessonId,
	uc.CourseId AS CourseId,
	ul.UserCourseId AS UserCourseId,
	ul.LessonId AS LessonId,
	ul.StartedDate AS StartedDate,
	ul.CompletedDate AS CompletedDate,
	ul.IsPassed AS IsPassed,
	ul.Result AS Result,
	ul.Attempt AS Attempt
	FROM User u
	JOIN UserLesson ul
	ON u.Id = ul.UserId
	JOIN UserCourse uc
	ON ul.UserCourseId = uc.Id;