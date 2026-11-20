# API Endpoint Plan

| HTTP Method | Route | Description | Role Required | Request Body | Expected Response |
|-------------|-------|-------------|---------------|--------------|-------------------|
| POST | /api/auth/register | Registers a new user | None | { email, password, role } | 201 Created |
| POST | /api/auth/login | Authenticates user and returns JWT token | None | { email, password } | 200 OK |
| GET | /api/users/profile | Retrieves logged-in user profile details | Any | None | 200 OK |
| PUT | /api/users/profile | Updates profile information | Any | { name, phone } | 200 OK |
| GET | /api/events | Lists all scheduled race events | None | None | 200 OK |
| POST | /api/events | Creates a new race event | Organiser | { title, date, location } | 201 Created |
| POST | /api/categories | Adds a category to an existing event | Organiser | { eventId, categoryName, distance } | 201 Created |
| POST | /api/enrolments | Registers a participant into a category | Participant | { categoryId } | 201 Created |
| GET | /api/results/{eventId} | Retrieves results for an event | None | None | 200 OK |
| POST | /api/results | Publishes participant event timings | Organiser | { enrolmentId, finishTime, position } | 201 Created |
