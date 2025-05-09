-- Inserção de usuários com hashes novos (BCrypt, custo 12)
INSERT INTO TB_USERS (USERNAME, PASSWORD_HASH, ACTIVE, ACCESS_GROUP) VALUES 
('joao.silva', '$2y$10$y3pjM8kD.voTFMS0qatp7OmUTrDjlA5uiT3sPeo6uClgmIrDF/Cba', '1', 1), -- senha123
('maria.lima', '$2y$10$RiSoDDjdVWcoy12kh0A08e9vAd7ULqgkgFITFStzyz7J0lNDKi/r6', '1', 2), -- teste123
('admin.user', '$2y$10$BTWilSQs2ytMoTORykDi9e2hQJ0wm.swJa8zls32KemvUuoKcG0FS', '1', 2), -- admin123
('usuario.teste', '$2y$10$zBWSypQTIt3nFKrRvavADu1KRh4BcskyNZN.PwFY0HxhJWbaJ2eke', '1', 3), -- user1234
('carla.oliveira', '$2y$10$jOzzoV9ObNdT8X03rHGr2.PIAV/B8Wl.gB3OvkvA.wYj3C6h98uky', '1', 3); -- abc123

INSERT INTO TB_ACCESS_GROUP (ID, NAME) VALUES
(1, 'ADMIN'),
(2, 'USER'),
(3, 'GUEST');