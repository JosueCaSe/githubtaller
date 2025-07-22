-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerModelo
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
        Modelo.Id,
        Modelo.IdMarca,
        Modelo.Nombre,
        Marca.Nombre AS Marca
    FROM dbo.Modelos Modelo
    INNER JOIN dbo.Marcas Marca
        ON Modelo.IdMarca = Marca.Id
    WHERE Modelo.Id = @Id;
END