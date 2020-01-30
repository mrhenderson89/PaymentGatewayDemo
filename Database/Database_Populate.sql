SET IDENTITY_INSERT RefPaymentStatus ON
INSERT INTO RefPaymentStatus ([Id],[Name])
VALUES (1,'Created')

INSERT INTO RefPaymentStatus ([Id],[Name])
VALUES (2,'Authorizing')

INSERT INTO RefPaymentStatus ([Id],[Name])
VALUES (3,'Complete')

INSERT INTO RefPaymentStatus ([Id],[Name])
VALUES (4,'Failed')

INSERT INTO RefPaymentStatus ([Id],[Name])
VALUES (5,'Error')
SET IDENTITY_INSERT RefPaymentStatus OFF

SET IDENTITY_INSERT Currency ON
INSERT INTO Currency ([Id],[Name],[Description])
VALUES (1,'GBP', 'Pound Sterling')

INSERT INTO Currency ([Id],[Name],[Description])
VALUES (2,'EUR', 'Euro')
SET IDENTITY_INSERT Currency OFF