using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInvalidUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Links\" WHERE \"OriginalUrl\" NOT LIKE 'http://%' AND \"OriginalUrl\" NOT LIKE 'https://%';");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Невозможно восстановить удаленные данные в рамках этой миграции,
            // так как удаление невалидных URL является необратимой операцией.
            // Рекомендуется создать резервную копию данных перед применением миграции.
        }
    }
}
