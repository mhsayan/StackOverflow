# **Stack Overflow  (Question Answer Platform)**

## <u>Features</u>

1. User can create account with **User Role**.

2. Only authenticated user can post question

3. Any authenticated user can answer the question

4. Any authenticated user can upvote or downvote

5. Only Answer seeker can select the Answer

6. Answer seeker can delete the question

7. Moderator can delete the Question and Answer

**Technology: ASP.NET Core 6.0 | EntityFramework Core 6.0 | MSSQL Server**

**Project Configuration**:

1. Create a new database

2. Change `DefaultConnectionString` in `appsettings.json`

3. Run this command from **Package Manager Console**:
   
   1. `dotnet ef database update --project StackOverflow.Web --context ApplicationDbContext`
   
   2. `dotnet ef database update --project StackOverflow.Web --context PlatformDbContext`

4. Run the project.

5. Create an ser account or use Moderator account.
   
   1. Moderator Email: moderator@stackoverflow.com
   
   2. Moderator Password: moderator@stackoverflow