
# REST Service Architecture for Restaurant Reservation System

## Key Functionalities & Endpoints

### User Management
- **Register User**
    - **Endpoint:** `POST /users`
- **Update User**
    - **Endpoint:** `PUT /users/{id}`
- **Delete User**
    - **Endpoint:** `DELETE /users/{id}`
- **Search Restaurants**
    - **Endpoint:** `GET /restaurants/search?query=Italian`
- **Make Reservation**
    - **Endpoint:** `POST /reservations`
- **Update Reservation**
    - **Endpoint:** `PUT /reservations/{id}`
- **Cancel Reservation**
    - **Endpoint:** `DELETE /reservations/{id}`
- **View Reservations**
    - **Endpoint:** `GET /reservations/user/{userId}`

### Administrator Management
- **Add Restaurant**
    - **Endpoint:** `POST /admin/restaurants`
- **Update Restaurant**
    - **Endpoint:** `PUT /admin/restaurants/{id}`
- **Delete Restaurant**
    - **Endpoint:** `DELETE /admin/restaurants/{id}`
- **View Reservations for a Restaurant**
    - **Endpoint:** `GET /admin/reservations/{restaurantId}`


