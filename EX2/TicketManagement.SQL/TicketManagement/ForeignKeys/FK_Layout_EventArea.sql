ALTER TABLE [dbo].[EventArea]
ADD CONSTRAINT FK_Layout_EventArea FOREIGN KEY (LayoutId)
    REFERENCES dbo.Layout (Id)