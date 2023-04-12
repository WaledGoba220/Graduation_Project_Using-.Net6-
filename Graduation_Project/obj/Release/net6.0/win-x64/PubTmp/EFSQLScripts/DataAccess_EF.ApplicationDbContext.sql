IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [TbContacts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TbContacts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20221122140319_Add_TbContacts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20221122140319_Add_TbContacts', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114194550_Update_ApplicationUser')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserName] nvarchar(256) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [UserName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114194550_Update_ApplicationUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Photo] varbinary(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114194550_Update_ApplicationUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230114194550_Update_ApplicationUser', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114201116_Update2_ApplicationUser')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserName] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230114201116_Update2_ApplicationUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230114201116_Update2_ApplicationUser', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230116110850_Update3_ApplicationUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [FullName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230116110850_Update3_ApplicationUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230116110850_Update3_ApplicationUser', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119085712_Add_TbSpecialization')
BEGIN
    CREATE TABLE [TbSpecialization] (
        [Id] int NOT NULL IDENTITY,
        [Name_EN] nvarchar(max) NOT NULL,
        [Name_AR] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TbSpecialization] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119085712_Add_TbSpecialization')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119085712_Add_TbSpecialization', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    ALTER TABLE [TbSpecialization] ADD [TbDoctorId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [TbDoctorId] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    CREATE TABLE [TbDoctors] (
        [Id] int NOT NULL IDENTITY,
        [Clinic] nvarchar(max) NULL,
        [Phone] int NOT NULL,
        [Brief] nvarchar(max) NOT NULL,
        [Country] nvarchar(max) NOT NULL,
        [City] nvarchar(max) NOT NULL,
        [Location] nvarchar(max) NULL,
        [SpecialId] int NOT NULL,
        [UserId] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TbDoctors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    CREATE INDEX [IX_TbSpecialization_TbDoctorId] ON [TbSpecialization] ([TbDoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    CREATE INDEX [IX_AspNetUsers_TbDoctorId] ON [AspNetUsers] ([TbDoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_TbDoctors_TbDoctorId] FOREIGN KEY ([TbDoctorId]) REFERENCES [TbDoctors] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    ALTER TABLE [TbSpecialization] ADD CONSTRAINT [FK_TbSpecialization_TbDoctors_TbDoctorId] FOREIGN KEY ([TbDoctorId]) REFERENCES [TbDoctors] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119124515_Add_TbDoctor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119124515_Add_TbDoctor', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119140353_Update_TbDoctor')
BEGIN
    EXEC sp_rename N'[TbDoctors].[UserId]', N'AppUserId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119140353_Update_TbDoctor')
BEGIN
    EXEC sp_rename N'[TbDoctors].[SpecialId]', N'SpecializationlId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119140353_Update_TbDoctor')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119140353_Update_TbDoctor', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119142933_Update_TbDoctor_2')
BEGIN
    EXEC sp_rename N'[TbDoctors].[SpecializationlId]', N'SpecializationId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119142933_Update_TbDoctor_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119142933_Update_TbDoctor_2', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_TbDoctors_TbDoctorId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    DROP INDEX [IX_AspNetUsers_TbDoctorId] ON [AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'TbDoctorId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [TbDoctorId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbDoctors]') AND [c].[name] = N'AppUserId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [TbDoctors] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [TbDoctors] ALTER COLUMN [AppUserId] nvarchar(450) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    CREATE INDEX [IX_TbDoctors_AppUserId] ON [TbDoctors] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    CREATE INDEX [IX_TbDoctors_SpecializationId] ON [TbDoctors] ([SpecializationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    ALTER TABLE [TbDoctors] ADD CONSTRAINT [FK_TbDoctors_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    ALTER TABLE [TbDoctors] ADD CONSTRAINT [FK_TbDoctors_TbSpecialization_SpecializationId] FOREIGN KEY ([SpecializationId]) REFERENCES [TbSpecialization] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119143434_Update_TbDoctor_3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119143434_Update_TbDoctor_3', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119170738_Add_TbClinicImage')
BEGIN
    CREATE TABLE [TbClinicImages] (
        [Id] int NOT NULL IDENTITY,
        [Image] varbinary(max) NOT NULL,
        [DoctorId] int NOT NULL,
        CONSTRAINT [PK_TbClinicImages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbClinicImages_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119170738_Add_TbClinicImage')
BEGIN
    CREATE INDEX [IX_TbClinicImages_DoctorId] ON [TbClinicImages] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230119170738_Add_TbClinicImage')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230119170738_Add_TbClinicImage', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122082350_Add_TbDiseaseType')
BEGIN
    CREATE TABLE [TbDiseaseTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TbDiseaseTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122082350_Add_TbDiseaseType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122082350_Add_TbDiseaseType', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122083246_Add_TbDisease')
BEGIN
    CREATE TABLE [TbDiseases] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [DiseaseTypeId] int NOT NULL,
        CONSTRAINT [PK_TbDiseases] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbDiseases_TbDiseaseTypes_DiseaseTypeId] FOREIGN KEY ([DiseaseTypeId]) REFERENCES [TbDiseaseTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122083246_Add_TbDisease')
BEGIN
    CREATE INDEX [IX_TbDiseases_DiseaseTypeId] ON [TbDiseases] ([DiseaseTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122083246_Add_TbDisease')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122083246_Add_TbDisease', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122182306_Add_TbAdvice')
BEGIN
    CREATE TABLE [TbAdvices] (
        [Id] int NOT NULL IDENTITY,
        [Image] varbinary(max) NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NULL,
        [DiseaseTypeId] int NOT NULL,
        [DiseaseId] int NULL,
        [DoctorId] int NULL,
        CONSTRAINT [PK_TbAdvices] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbAdvices_TbDiseases_DiseaseId] FOREIGN KEY ([DiseaseId]) REFERENCES [TbDiseases] ([Id]),
        CONSTRAINT [FK_TbAdvices_TbDiseaseTypes_DiseaseTypeId] FOREIGN KEY ([DiseaseTypeId]) REFERENCES [TbDiseaseTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TbAdvices_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122182306_Add_TbAdvice')
BEGIN
    CREATE INDEX [IX_TbAdvices_DiseaseId] ON [TbAdvices] ([DiseaseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122182306_Add_TbAdvice')
BEGIN
    CREATE INDEX [IX_TbAdvices_DiseaseTypeId] ON [TbAdvices] ([DiseaseTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122182306_Add_TbAdvice')
BEGIN
    CREATE INDEX [IX_TbAdvices_DoctorId] ON [TbAdvices] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122182306_Add_TbAdvice')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122182306_Add_TbAdvice', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183409_Update_TbDiseaseType')
BEGIN
    EXEC sp_rename N'[TbDiseaseTypes].[Name]', N'Name_EN', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183409_Update_TbDiseaseType')
BEGIN
    ALTER TABLE [TbDiseaseTypes] ADD [Name_AR] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183409_Update_TbDiseaseType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122183409_Update_TbDiseaseType', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183520_Update_TbDisease')
BEGIN
    EXEC sp_rename N'[TbDiseases].[Name]', N'Name_EN', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183520_Update_TbDisease')
BEGIN
    ALTER TABLE [TbDiseases] ADD [Name_AR] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230122183520_Update_TbDisease')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230122183520_Update_TbDisease', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230126085951_Update_TbAdvice')
BEGIN
    ALTER TABLE [TbAdvices] ADD [AppUserId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230126085951_Update_TbAdvice')
BEGIN
    CREATE INDEX [IX_TbAdvices_AppUserId] ON [TbAdvices] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230126085951_Update_TbAdvice')
BEGIN
    ALTER TABLE [TbAdvices] ADD CONSTRAINT [FK_TbAdvices_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230126085951_Update_TbAdvice')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230126085951_Update_TbAdvice', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124241_Add_TbComment')
BEGIN
    CREATE TABLE [TbComments] (
        [Id] int NOT NULL IDENTITY,
        [Comment] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NULL,
        CONSTRAINT [PK_TbComments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124241_Add_TbComment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230127124241_Add_TbComment', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    ALTER TABLE [TbComments] ADD [AdviceId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    ALTER TABLE [TbComments] ADD [AppUserId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    CREATE INDEX [IX_TbComments_AdviceId] ON [TbComments] ([AdviceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    CREATE INDEX [IX_TbComments_AppUserId] ON [TbComments] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    ALTER TABLE [TbComments] ADD CONSTRAINT [FK_TbComments_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    ALTER TABLE [TbComments] ADD CONSTRAINT [FK_TbComments_TbAdvices_AdviceId] FOREIGN KEY ([AdviceId]) REFERENCES [TbAdvices] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127124456_Add_TbComment_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230127124456_Add_TbComment_1', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127141002_Add_TbReplay')
BEGIN
    CREATE TABLE [TbReplays] (
        [Id] int NOT NULL IDENTITY,
        [Replay] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NULL,
        [CommentId] int NOT NULL,
        [AppUserId] nvarchar(450) NOT NULL,
        [AdviceId] int NOT NULL,
        CONSTRAINT [PK_TbReplays] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbReplays_AspNetUsers_AppUserId] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TbReplays_TbAdvices_AdviceId] FOREIGN KEY ([AdviceId]) REFERENCES [TbAdvices] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TbReplays_TbComments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [TbComments] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127141002_Add_TbReplay')
BEGIN
    CREATE INDEX [IX_TbReplays_AdviceId] ON [TbReplays] ([AdviceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127141002_Add_TbReplay')
BEGIN
    CREATE INDEX [IX_TbReplays_AppUserId] ON [TbReplays] ([AppUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127141002_Add_TbReplay')
BEGIN
    CREATE INDEX [IX_TbReplays_CommentId] ON [TbReplays] ([CommentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230127141002_Add_TbReplay')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230127141002_Add_TbReplay', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbReplays]') AND [c].[name] = N'CreationDateTime');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [TbReplays] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [TbReplays] ALTER COLUMN [CreationDateTime] datetime2 NOT NULL;
    ALTER TABLE [TbReplays] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [CreationDateTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbComments]') AND [c].[name] = N'CreationDateTime');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [TbComments] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [TbComments] ALTER COLUMN [CreationDateTime] datetime2 NOT NULL;
    ALTER TABLE [TbComments] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [CreationDateTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbAdvices]') AND [c].[name] = N'CreationDateTime');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [TbAdvices] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [TbAdvices] ALTER COLUMN [CreationDateTime] datetime2 NOT NULL;
    ALTER TABLE [TbAdvices] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [CreationDateTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    CREATE TABLE [TbDoctorViewsCounts] (
        [Id] int NOT NULL IDENTITY,
        [DoctorId] int NOT NULL,
        [Count] int NOT NULL,
        CONSTRAINT [PK_TbDoctorViewsCounts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbDoctorViewsCounts_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    CREATE INDEX [IX_TbDoctorViewsCounts_DoctorId] ON [TbDoctorViewsCounts] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230130184824_Add_TbDoctorViewsCount')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230130184824_Add_TbDoctorViewsCount', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131082359_Add_TbRating')
BEGIN
    CREATE TABLE [TbRatings] (
        [Id] int NOT NULL IDENTITY,
        [Rate] tinyint NOT NULL,
        [DoctorId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TbRatings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbRatings_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TbRatings_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131082359_Add_TbRating')
BEGIN
    CREATE INDEX [IX_TbRatings_DoctorId] ON [TbRatings] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131082359_Add_TbRating')
BEGIN
    CREATE INDEX [IX_TbRatings_UserId] ON [TbRatings] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230131082359_Add_TbRating')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230131082359_Add_TbRating', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230202215025_Update_ApplicationUser-1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230202215025_Update_ApplicationUser-1', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230205193728_Add_TbRegistrationRequests')
BEGIN
    CREATE TABLE [TdRegistrationRequests] (
        [Id] int NOT NULL IDENTITY,
        [TotalRegistrations] int NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_TdRegistrationRequests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230205193728_Add_TbRegistrationRequests')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230205193728_Add_TbRegistrationRequests', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230205194806_Update_ApplicationUser_AddDate')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230205194806_Update_ApplicationUser_AddDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230205194806_Update_ApplicationUser_AddDate', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230207195838_Add_TbAdviceRequests')
BEGIN
    CREATE TABLE [TbAdviceRequests] (
        [Id] int NOT NULL IDENTITY,
        [TotalAdvices] int NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_TbAdviceRequests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230207195838_Add_TbAdviceRequests')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230207195838_Add_TbAdviceRequests', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230215191557_Add_TbPneumonia')
BEGIN
    CREATE TABLE [TbPneumonias] (
        [Id] int NOT NULL IDENTITY,
        [ImageName] nvarchar(max) NOT NULL,
        [FullName] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TbPneumonias] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbPneumonias_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230215191557_Add_TbPneumonia')
BEGIN
    CREATE INDEX [IX_TbPneumonias_UserId] ON [TbPneumonias] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230215191557_Add_TbPneumonia')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230215191557_Add_TbPneumonia', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230216121457_Update_TbPneumonia')
BEGIN
    EXEC sp_rename N'[TbPneumonias].[FullName]', N'PatientName', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230216121457_Update_TbPneumonia')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230216121457_Update_TbPneumonia', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230216124226_Add_TbTuberculosis')
BEGIN
    CREATE TABLE [TbTuberculosis] (
        [Id] int NOT NULL IDENTITY,
        [ImageName] nvarchar(max) NOT NULL,
        [PatientName] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TbTuberculosis] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbTuberculosis_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230216124226_Add_TbTuberculosis')
BEGIN
    CREATE INDEX [IX_TbTuberculosis_UserId] ON [TbTuberculosis] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230216124226_Add_TbTuberculosis')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230216124226_Add_TbTuberculosis', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbPneumonias] DROP CONSTRAINT [FK_TbPneumonias_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbTuberculosis] DROP CONSTRAINT [FK_TbTuberculosis_AspNetUsers_UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    DROP INDEX [IX_TbTuberculosis_UserId] ON [TbTuberculosis];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    DROP INDEX [IX_TbPneumonias_UserId] ON [TbPneumonias];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbTuberculosis]') AND [c].[name] = N'UserId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [TbTuberculosis] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [TbTuberculosis] DROP COLUMN [UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TbPneumonias]') AND [c].[name] = N'UserId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [TbPneumonias] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [TbPneumonias] DROP COLUMN [UserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbTuberculosis] ADD [DoctorId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbPneumonias] ADD [DoctorId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    CREATE INDEX [IX_TbTuberculosis_DoctorId] ON [TbTuberculosis] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    CREATE INDEX [IX_TbPneumonias_DoctorId] ON [TbPneumonias] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbPneumonias] ADD CONSTRAINT [FK_TbPneumonias_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    ALTER TABLE [TbTuberculosis] ADD CONSTRAINT [FK_TbTuberculosis_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230218080712_Update_TbPneumonia_TbTuberculosis')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230218080712_Update_TbPneumonia_TbTuberculosis', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230219191924_Add_TbLungCancer')
BEGIN
    CREATE TABLE [TbLungCancer] (
        [Id] int NOT NULL IDENTITY,
        [PatientName] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [Gender] int NOT NULL,
        [AirPollution] int NOT NULL,
        [Alcoholuse] int NOT NULL,
        [DustAllergy] int NOT NULL,
        [OccuPationalHazards] int NOT NULL,
        [GeneticRisk] int NOT NULL,
        [ChronicLungDisease] int NOT NULL,
        [BalancedDiet] int NOT NULL,
        [Obesity] int NOT NULL,
        [Smoking] int NOT NULL,
        [PassiveSmoker] int NOT NULL,
        [ChestPain] int NOT NULL,
        [CoughingofBlood] int NOT NULL,
        [Fatigue] int NOT NULL,
        [WeightLoss] int NOT NULL,
        [ShortnessofBreath] int NOT NULL,
        [Wheezing] int NOT NULL,
        [SwallowingDifficulty] int NOT NULL,
        [ClubbingofFingerNail] int NOT NULL,
        [FrequentCold] int NOT NULL,
        [DryCough] int NOT NULL,
        [Snoring] int NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NOT NULL,
        [DoctorId] int NOT NULL,
        CONSTRAINT [PK_TbLungCancer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbLungCancer_TbDoctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [TbDoctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230219191924_Add_TbLungCancer')
BEGIN
    CREATE INDEX [IX_TbLungCancer_DoctorId] ON [TbLungCancer] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230219191924_Add_TbLungCancer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230219191924_Add_TbLungCancer', N'6.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230227193315_Add_TbMeasuringBox')
BEGIN
    CREATE TABLE [TbMeasuringBox] (
        [Id] int NOT NULL IDENTITY,
        [Temperature] real NULL,
        [Oxygen] real NULL,
        [HeartRate] real NULL,
        [PatientName] nvarchar(max) NOT NULL,
        [CreationDateTime] datetime2 NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_TbMeasuringBox] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TbMeasuringBox_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230227193315_Add_TbMeasuringBox')
BEGIN
    CREATE INDEX [IX_TbMeasuringBox_UserId] ON [TbMeasuringBox] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230227193315_Add_TbMeasuringBox')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230227193315_Add_TbMeasuringBox', N'6.0.11');
END;
GO

COMMIT;
GO

