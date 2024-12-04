insert into dbo.AspNetRoles(Id,Name,NormalizedName)
values(NEWID(),'Admin','ADMIN'),
(NEWID(),'Manager','MANAGER'),
(NEWID(),'Staff','STAFF'),
(NEWID(),'Customer','CUSTOMER')