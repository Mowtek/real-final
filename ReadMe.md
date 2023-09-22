Student Name: שרון
Student Class Number: D130222ER
Email: Mowsplace@gmail.com

## Welcome to Zwizzer!

Welcome to the best, Never-Seen-Before Social Media called Zwizzer!
This project was created as a Final assignment for my Full-Stack course in HackerU.
To run it, make sure to use "update-database" on the ZwizzerDAL project in the ZwizzerBackend Solution Package Manager Console.
After doing so, you can run "npm start" on the Zwizzer folder, and you'll have access to the project.
This README file will walk you through some of the required featured.

**Client Side Requirements:**

1. **Landing Page**: The first page you see on the site is the Home page, where you can see all the "Zweets" added by other users. If logged in (by using the "Login" NavLink {or by using the login button on the bottom navbar for phone users}) the home page will show a section to post a new Zweet.
2. **NavBar**: As mentioned before, phone users will have a NavBar at all routes, sticked to the bottom of the page. PC/Laptop users will have a static sticky navbar on the left side of the page.
3. **Login**: If you press the "Login" button on the bottom left side of the screen, you will be routed to the "/login" page, where you can enter a Username and Password, and to login to the site. There is a default user to check admin features, which is Username: Admin, Password: Admin.
4. **CRUD**: The CRUD operations are as follows: CREATE - any user (logged in) can create a new post on the "Home" page, and any logged out user can register a new user and create new posts with. READ - the home page automatically reads all the "Zweets" that are in the database, whether you're logged in or not. UPDATE - an admin has the option "Manage Users" in his nav-bar, where he's allowed to update any user info in the database. DELETE - an admin also has the option to delete any tweet he chooses. To delete a post, click on the post you'd like to delete (get routed to /post/{postId}) and press the red X button (must be admin).
5. **Search option**: In any navbar, logged in or not you can see a Search option, whether as text or icon. If you press this option, you will be routed to the search page, where you can enter any keyword you'd like, and if there are any "Zweets" containing that keyword, they will show up on the screen.
6. **Information Page**: To dive into any information you'd like to receive, you can press either on the Posts author name / profile picture, and you'll get routed to their profile page, where you can see his Bio, Zweets, Likes and more. If instead you press on the post itself (meaning anywhere other than the authos name/picture), you'll get routed to the post page, where you will see the post itself, amount of likes/comments/rezweets, the comments on the post and even have the ability to like/rezweet the post! (as i said never seen before)
7. **Seperation of Pages**: React Router has been used in this project, for the purpose of fluently moving through pages. The navbar and Footer are static, all other pages are routed through React Router.
8. **Validations**: All input fields are validated through the Front-End, so there would be no error whatsoever the user can make to break something in the Back-End.

**Server Side Requirements:**

1.  **Modularity**: There are different controllers for any action you want to make, either for Users or for the Zweets. There is also a DAL to make it separated from the API itself.
2.  **EF Core usage**: The whole project is built on EF Core.
3.  **Authorization**: JWT has been used for Authorization and Authentication.
4.  **Validations**: All server side operations go through validations with the ModelState, so that even if the user managed to pass the Front-End validations, we would still catch code-breaking inputs.

That is all. Hope you enjoy this project as much as I enjoyed creating it.
There is much more work to be done, but unfortunately for time constraints I weren't able to create all of the stuff I planned for. I will wait for the grade on my assignment, and continue building all the features I wanted.

**Here are some default users that were seeded with the first run of the project:**
Username: Admin | Password: Admin
Username: Sharon | Password: VeryGood123
Username: SickGamer420 | Password: TheBestEver
