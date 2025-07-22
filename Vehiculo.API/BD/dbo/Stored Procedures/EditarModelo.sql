-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EditarModelo
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier,
	@IdMarca AS uniqueidentifier,
	@Nombre AS varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		UPDATE [dbo].[Modelos]
		  SET [IdMarca] = @IdMarca,
		      [Nombre] = @Nombre
		 WHERE Id=@Id
		 SELECT @Id
	 COMMIT TRANSACTION
END