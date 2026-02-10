-- Inserir dados de teste para LH_PET

USE lh_pet_db;

-- 1. Limpar dados existentes (opcional)
DELETE FROM Consulta;
DELETE FROM Animal;
DELETE FROM Cliente;
DELETE FROM User;
DELETE FROM Fornecedor;

-- 2. Inserir Usuários de Teste
INSERT INTO User (Username, Email, PasswordHash, DataCadastro) VALUES
('owner', 'owner@lhpet.com', '$2a$11$e2Y.KJlKHmyS8rkGlkdAWe5xWnRvuM9xhYeNMzKRk.HvJBLyKPRWO', NOW()), -- senha: Senha@123
('veterinario', 'vet@lhpet.com', '$2a$11$e2Y.KJlKHmyS8rkGlkdAWe5xWnRvuM9xhYeNMzKRk.HvJBLyKPRWO', NOW()); -- senha: Senha@123

-- 3. Inserir Clientes com CPF único
INSERT INTO Cliente (Nome, CPF, Email, DataCadastro) VALUES
('João Silva Santos', '12345678901', 'joao@email.com', NOW()),
('Maria Oliveira Costa', '10987654321', 'maria@email.com', NOW()),
('Pedro Ferreira Silva', '11223344556', 'pedro@email.com', NOW()),
('Ana Carolina Santos', '55443322110', 'ana@email.com', NOW()),
('Carlos Alberto Lima', '99887766554', 'carlos@email.com', NOW());

-- 4. Inserir Animais
INSERT INTO Animal (Nome, Tipo, Sexo, Raca, Idade, ClienteID, DataCadastro) VALUES
-- Animais do João (ClienteID 1)
('Max', 'Cachorro', 'Macho', 'Labrador', '3', 1, NOW()),
('Bella', 'Gato', 'Fêmea', 'Siamês', '2', 1, NOW()),

-- Animais da Maria (ClienteID 2)
('Rex', 'Cachorro', 'Macho', 'Pinscher', '5', 2, NOW()),
('Luna', 'Cachorro', 'Fêmea', 'Golden Retriever', '4', 2, NOW()),
('Whiskers', 'Gato', 'Macho', 'Persa', '6', 2, NOW()),

-- Animais do Pedro (ClienteID 3)
('Poppy', 'Cachorro', 'Fêmea', 'Bulldog', '2', 3, NOW()),
('Duke', 'Cachorro', 'Macho', 'Pastor Alemão', '7', 3, NOW()),

-- Animais da Ana (ClienteID 4)
('Miau', 'Gato', 'Fêmea', 'Vira-lata', '1', 4, NOW()),
('Spike', 'Cachorro', 'Macho', 'Boxer', '3', 4, NOW()),

-- Animais do Carlos (ClienteID 5)
('Charlie', 'Cachorro', 'Macho', 'Poodle', '4', 5, NOW()),
('Daisy', 'Gato', 'Fêmea', 'Maine Coon', '2', 5, NOW());

-- 5. Inserir Consultas Agendadas (próximas 2 semanas)
INSERT INTO Consulta (ClienteID, AnimalID, DataHora, Descricao) VALUES
(1, 1, DATE_ADD(NOW(), INTERVAL 2 DAY), 'Vacinação e check-up geral'),
(1, 2, DATE_ADD(NOW(), INTERVAL 5 DAY), 'Limpeza de dentes'),
(2, 3, DATE_ADD(NOW(), INTERVAL 1 DAY), 'Consulta periódica'),
(2, 4, DATE_ADD(NOW(), INTERVAL 7 DAY), 'Banho e tosa'),
(2, 5, DATE_ADD(NOW(), INTERVAL 3 DAY), 'Vermifugação'),
(3, 6, DATE_ADD(NOW(), INTERVAL 6 DAY), 'Exame dermatológico'),
(3, 7, DATE_ADD(NOW(), INTERVAL 10 DAY), 'Vacinação de reforço'),
(4, 8, DATE_ADD(NOW(), INTERVAL 4 DAY), 'Consulta pós-cirurgia'),
(4, 9, DATE_ADD(NOW(), INTERVAL 8 DAY), 'Tratamento de alergia'),
(5, 10, DATE_ADD(NOW(), INTERVAL 2 DAY), 'Consulta de seguimento'),
(5, 11, DATE_ADD(NOW(), INTERVAL 9 DAY), 'Vacinação anual');

-- 6. Inserir Fornecedores com CNPJ único
INSERT INTO Fornecedor (Nome, CNPJ, Email) VALUES
('Ração Premium Ltda', '12345678000195', 'vendas@racao.com'),
('MediVet Farmácia', '98765432000187', 'farmacia@medivet.com'),
('Equipamentos Clínicos Inc', '56789012000134', 'vendas@equip.com'),
('Higiene Pet Supplies', '34567890000156', 'contato@higienepet.com');

-- 7. Exibir resumo dos dados inseridos
SELECT 
  'Clientes' as Tabela, COUNT(*) as Registros FROM Cliente
UNION ALL
SELECT 
  'Animais', COUNT(*) FROM Animal
UNION ALL
SELECT 
  'Consultas', COUNT(*) FROM Consulta
UNION ALL
SELECT 
  'Usuários', COUNT(*) FROM User
UNION ALL
SELECT 
  'Fornecedores', COUNT(*) FROM Fornecedor;
