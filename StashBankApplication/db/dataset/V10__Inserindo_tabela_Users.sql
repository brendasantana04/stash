INSERT INTO users
(
    username,
    firstName,
    lastName,
    email,
    phoneNumber,
    password,
    refresh_token,
    refresh_token_expiry_time
)
VALUES
(
    'beatriz',
    'Beatriz',
    'Terra',
    'beatriz@email.com',
    '11987654321',
    '123456',
    NULL,
    NULL
),
(
    'celline',
    'Celline',
    'Bitencourt',
    'celline@email.com',
    '11912345678',
    '123456',
    NULL,
    NULL
),
(
    'brenda',
    'Brenda',
    'Santana',
    'brenda@email.com',
    '11999998888',
    '123456',
    NULL,
    NULL
);

UPDATE users
SET refresh_token = 'mock_refresh_token_123',
    refresh_token_expiry_time = DATEADD(day, 7, GETDATE())
WHERE username = 'beatriz';