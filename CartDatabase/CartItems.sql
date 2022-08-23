CREATE TABLE [dbo].[CartItems]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CartID] INT NOT NULL, 
    [ProductID] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [FK_CartItems_ToTable] FOREIGN KEY ([CartID]) REFERENCES [Cart]([Id])
)
