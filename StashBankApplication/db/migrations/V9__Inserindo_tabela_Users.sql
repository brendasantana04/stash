CREATE TABLE users (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,

    username VARCHAR(20) NOT NULL,
    firstName VARCHAR(40) NOT NULL,
    lastName VARCHAR(40) NOT NULL,

    email VARCHAR(30) NOT NULL,
    phoneNumber VARCHAR(11) NOT NULL,

    password VARCHAR(30) NOT NULL,

    refresh_token VARCHAR(255) NULL,
    refresh_token_expiry_time DATETIME2 NULL
);