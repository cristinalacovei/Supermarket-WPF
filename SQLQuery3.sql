CREATE TABLE Producatori (
    ProducatorID INT PRIMARY KEY IDENTITY,
    Nume NVARCHAR(100) NOT NULL,
    TaraOrigine NVARCHAR(50) NOT NULL
);

CREATE TABLE Categorii (
    CategorieID INT PRIMARY KEY IDENTITY,
    Nume NVARCHAR(50) NOT NULL
);

CREATE TABLE Produse (
    ProdusID INT PRIMARY KEY IDENTITY,
    Nume NVARCHAR(100) NOT NULL,
    CodBare NVARCHAR(50) NOT NULL,
    CategorieID INT FOREIGN KEY REFERENCES Categorii(CategorieID),
    ProducatorID INT FOREIGN KEY REFERENCES Producatori(ProducatorID)
);

CREATE TABLE Stocuri (
    StocID INT PRIMARY KEY IDENTITY,
    ProdusID INT FOREIGN KEY REFERENCES Produse(ProdusID),
    Cantitate DECIMAL(10,2) NOT NULL,
    UnitateMasura NVARCHAR(20) NOT NULL,
    DataAprovizionare DATE NOT NULL,
    DataExpirare DATE,
    PretAchizitie DECIMAL(10,2) NOT NULL,
    PretVanzare DECIMAL(10,2) NOT NULL
);

CREATE TABLE Utilizatori (
    UtilizatorID INT PRIMARY KEY IDENTITY,
    NumeUtilizator NVARCHAR(50) NOT NULL,
    Parola NVARCHAR(50) NOT NULL,
    TipUtilizator NVARCHAR(20) NOT NULL
);

CREATE TABLE BonuriCasa (
    BonID INT PRIMARY KEY IDENTITY,
    DataEliberare DATE NOT NULL,
    CasierID INT FOREIGN KEY REFERENCES Utilizatori(UtilizatorID),
    SumaIncasata DECIMAL(10,2) NOT NULL
);

CREATE TABLE ProduseBon (
    ProdusBonID INT PRIMARY KEY IDENTITY,
    BonID INT FOREIGN KEY REFERENCES BonuriCasa(BonID),
    ProdusID INT FOREIGN KEY REFERENCES Produse(ProdusID),
    Cantitate DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL
);

CREATE TABLE Oferte (
    OfertaID INT PRIMARY KEY IDENTITY,
    Motiv NVARCHAR(200),
    ProdusID INT FOREIGN KEY REFERENCES Produse(ProdusID),
    ProcentReducere DECIMAL(5,2),
    DataInceput DATE,
    DataSfarsit DATE
);
