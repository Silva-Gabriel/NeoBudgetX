-- Inserção de usuários com hashes novos (BCrypt, custo 12)
INSERT INTO TB_USERS (USERNAME, PASSWORD_HASH) VALUES 
('joao.silva', '$2y$10$y3pjM8kD.voTFMS0qatp7OmUTrDjlA5uiT3sPeo6uClgmIrDF/Cba'), -- senha123
('maria.lima', '$2y$10$RiSoDDjdVWcoy12kh0A08e9vAd7ULqgkgFITFStzyz7J0lNDKi/r6'), -- teste123
('admin.user', '$2y$10$BTWilSQs2ytMoTORykDi9e2hQJ0wm.swJa8zls32KemvUuoKcG0FS'), -- admin123
('usuario.teste', '$2y$10$zBWSypQTIt3nFKrRvavADu1KRh4BcskyNZN.PwFY0HxhJWbaJ2eke'), -- user1234
('carla.oliveira', '$2y$10$jOzzoV9ObNdT8X03rHGr2.PIAV/B8Wl.gB3OvkvA.wYj3C6h98uky'); -- abc123