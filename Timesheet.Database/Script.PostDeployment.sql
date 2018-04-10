/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DECLARE @ManagerId INT
DECLARE @ManagerName VARCHAR(255)
DECLARE @ManagerEmailAddress VARCHAR(255)
DECLARE @ManagerPassword VARCHAR(255)
SET @ManagerName = 'Mr Manager'
SET @ManagerEmailAddress = 'test@test.com'
SET @ManagerPassword = '8B2085F74DFA9C78A23B7D573C23D27D6D0B0E50C82A9B13138B193325BE3814'

INSERT INTO [dbo].[Users]([UserName], [Password]) VALUES (@ManagerName, @ManagerPassword);
SET @ManagerId = SCOPE_IDENTITY();
INSERT INTO [dbo].[Managers] ([UserId], [EmailAddress]) VALUES (@ManagerId, @ManagerEmailAddress)

GO