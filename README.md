# Simple Device API with Swagger

This is a simple API for managing devices, exposing endpoints for CRUD operations on devices. This API is powered by Swagger for easy exploration and testing.

## Endpoints

### 1. Get Device by ID

**Endpoint:**
- `GET /api/devices/{id}`

**Description:**
Retrieve details of a device by its unique identifier.

### 2. Get All Devices

**Endpoint:**
- `GET /api/devices`

**Query Parameters:**
- `brand` (optional): Filter devices by brand.

**Description:**
Retrieve a list of all devices. Optionally, you can filter devices by providing the `brand` query parameter.

### 3. Create Device

**Endpoint:**
- `POST /api/devices`

**Request Body:**
- `Name` (string): Name of the device.
- `Brand` (string): Brand of the device.

**Description:**
Create a new device with the specified name and brand.

### 4. Update Device

**Endpoint:**
- `PUT /api/devices/{id}`

**Request Body:**
- `Name` (string): New name for the device.
- `Brand` (string): New brand for the device.

**Description:**
Update the details of a device by providing a new name and brand.

### 5. Patch Device

**Endpoint:**
- `PATCH /api/devices/{id}`

**Request Body:**
- JSON Patch document format for partial updates.

**Description:**
Apply partial updates to a device using the JSON Patch document format.

### 6. Delete Device

**Endpoint:**
- `DELETE /api/devices/{id}`

**Description:**
Delete a device by its unique identifier.

## Swagger Documentation

Explore and test the API interactively using Swagger. Visit the Swagger UI at `/swagger` when running the application.

## Usage

To interact with this API, you can use various tools like `curl`, `Postman`, or your preferred HTTP client. Make sure to follow the provided endpoint descriptions for request formats.

### Examples:

- **Get Device by ID:**
GET /api/devices/123

- **Get All Devices:**
GET /api/devices?brand=mybrand

- **Create Device:**
POST /api/devices
Request Body:
```json
{
  "Name": "NewDevice",
  "Brand": "NewBrand"
}

- **Update Device:**
PUT /api/devices/123
Request Body:
```json
{
"Name": "UpdatedDeviceName",
"Brand": "UpdatedBrand"
}

- **Patch Device:**
PATCH /api/devices/123
Request Body:
```json
[
  { "op": "replace", "path": "/name", "value": "NewName" },
  { "op": "replace", "path": "/brand", "value": "NewBrand" }
]

- **Delete Device:**
DELETE /api/devices/123

