USE [master]
GO
/****** Object:  Database [Covid]    Script Date: 8/05/2022 05:08:58 ******/
CREATE DATABASE [Covid]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Covid', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Covid.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Covid_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Covid_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Covid] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Covid].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Covid] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Covid] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Covid] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Covid] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Covid] SET ARITHABORT OFF 
GO
ALTER DATABASE [Covid] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Covid] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Covid] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Covid] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Covid] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Covid] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Covid] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Covid] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Covid] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Covid] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Covid] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Covid] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Covid] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Covid] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Covid] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Covid] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Covid] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Covid] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Covid] SET  MULTI_USER 
GO
ALTER DATABASE [Covid] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Covid] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Covid] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Covid] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Covid] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Covid] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Covid] SET QUERY_STORE = OFF
GO
USE [Covid]
GO
/****** Object:  Table [dbo].[CuadroClinico]    Script Date: 8/05/2022 05:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuadroClinico](
	[CuadroClinicoId] [int] IDENTITY(1,1) NOT NULL,
	[PersonaId] [int] NOT NULL,
	[UsuarioId] [int] NULL,
	[Fecha] [datetime] NOT NULL,
	[TipoMonitoreo] [nvarchar](20) NULL,
	[FuncionesVitales] [xml] NULL,
	[SignosSintomas] [xml] NULL,
	[SintomasAlarma] [xml] NULL,
	[Estado] [nvarchar](20) NULL,
	[Observaciones] [nvarchar](200) NULL,
	[Costo] [decimal](18, 2) NULL,
	[Antecedentes] [varchar](250) NULL,
	[Examen] [varchar](25) NULL,
	[Diagnostico] [varchar](100) NULL,
 CONSTRAINT [PK_CuadroClinico] PRIMARY KEY CLUSTERED 
(
	[CuadroClinicoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 8/05/2022 05:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[PersonaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [nvarchar](50) NULL,
	[Apellidos] [nvarchar](50) NULL,
	[DNI] [char](8) NULL,
	[NroCelular] [varchar](12) NULL,
	[EstadoCivil] [nvarchar](10) NULL,
	[FechaNacimiento] [date] NULL,
	[Sexo] [char](1) NULL,
	[TelefonoEmergencia] [varchar](12) NULL,
	[CorreoElectronico] [nvarchar](50) NULL,
	[CondicionDeRiesgo] [nvarchar](100) NULL,
	[Vacunas] [varchar](2) NULL,
	[Alergias] [varchar](50) NULL,
	[Ocupacion] [nvarchar](50) NULL,
	[Responsable] [nvarchar](100) NULL,
	[Talla] [decimal](18, 2) NULL,
	[Peso] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[PersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receta]    Script Date: 8/05/2022 05:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receta](
	[RecetaId] [int] IDENTITY(1,1) NOT NULL,
	[CuadroClinicoId] [int] NULL,
	[Medicamento] [varchar](50) NULL,
	[Dosis] [varchar](50) NULL,
	[Duracion] [varchar](10) NULL,
	[Cantidad] [int] NULL,
 CONSTRAINT [PK_Receta] PRIMARY KEY CLUSTERED 
(
	[RecetaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo]    Script Date: 8/05/2022 05:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo](
	[TipoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](30) NULL,
 CONSTRAINT [PK_Tipo] PRIMARY KEY CLUSTERED 
(
	[TipoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/05/2022 05:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[TipoId] [int] NOT NULL,
	[DNI] [char](8) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[Apellidos] [nvarchar](50) NOT NULL,
	[NombreUsuario] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](12) NOT NULL,
	[Especialidad] [nvarchar](20) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CuadroClinico]  WITH CHECK ADD  CONSTRAINT [FK_CuadroClinico_Persona] FOREIGN KEY([PersonaId])
REFERENCES [dbo].[Persona] ([PersonaId])
GO
ALTER TABLE [dbo].[CuadroClinico] CHECK CONSTRAINT [FK_CuadroClinico_Persona]
GO
ALTER TABLE [dbo].[CuadroClinico]  WITH CHECK ADD  CONSTRAINT [FK_CuadroClinico_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[CuadroClinico] CHECK CONSTRAINT [FK_CuadroClinico_Usuario]
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD  CONSTRAINT [FK_Receta_CuadroClinico] FOREIGN KEY([CuadroClinicoId])
REFERENCES [dbo].[CuadroClinico] ([CuadroClinicoId])
GO
ALTER TABLE [dbo].[Receta] CHECK CONSTRAINT [FK_Receta_CuadroClinico]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Personal_Tipo] FOREIGN KEY([TipoId])
REFERENCES [dbo].[Tipo] ([TipoId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Personal_Tipo]
GO
USE [master]
GO
ALTER DATABASE [Covid] SET  READ_WRITE 
GO
