# Dạy Học Trực Tuyến (DayHocTrucTuyen)

Website quản lý dạy và học trực tuyến xây dựng trên nền tảng **ASP.NET Core 9.0 MVC** và **Entity Framework Core 9**, hỗ trợ quản lý lớp học, đăng bài viết & tài liệu, thi trắc nghiệm trực tuyến tự động chấm điểm, nhắn tin thời gian thực với **SignalR**, nạp ví & thanh toán **MoMo**, đăng nhập **Google OAuth** và trang quản trị hệ thống.

---

## 1. Công nghệ sử dụng

- **Backend:** ASP.NET Core 9.0 MVC (`net9.0`), Entity Framework Core 9.0.2
- **Database:** Microsoft SQL Server
- **Real-time:** SignalR (`ChatHub`)
- **Authentication:** Cookie Authentication + Google OAuth 2.0
- **Payment:** MoMo Payment Gateway API (`Newtonsoft.Json`)
- **Frontend:** Razor Views, Bootstrap 5, jQuery
- **Container:** Docker (`.NET 9` multi-stage build)

---

## 2. Yêu cầu môi trường

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Microsoft SQL Server (SQL Server 2019/2022, Express hoặc LocalDB) & SSMS / Azure Data Studio
- Visual Studio 2022 (v17.12+) hoặc VS Code
- Docker Desktop (nếu chạy bằng container)

---

## 3. Hướng dẫn cài đặt & Import Database

### Bước 1: Clone repository

```bash
git clone https://github.com/harmony-205/DayHocTrucTuyen.git
cd DayHocTrucTuyen
```

### Bước 2: Import Database Script

1. Mở **SQL Server Management Studio (SSMS)** và kết nối tới SQL Server Instance của bạn.
2. Mở file script tại đường dẫn:
   ```text
   ./wwwroot/Content/SQL/script.sql
   ```
3. Nhấn **Execute (F5)** để tạo cơ sở dữ liệu `DayHocTrucTuyen`, toàn bộ bảng và dữ liệu mẫu ban đầu.

### Bước 3: Cấu hình Connection String & Email trong `appsettings.json`

Mở file `./appsettings.json` và cập nhật chuỗi kết nối phù hợp với SQL Server trên máy của bạn:

```json
{
  "ConnectionStrings": {
    "DayHocTrucTuyen": "Server=localhost;Database=DayHocTrucTuyen;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "Email": {
    "Address": "<your-email@gmail.com>",
    "Password": "<16-char-google-app-password>"
  }
}
```

- **Lưu ý SQL Server:**
  - Nếu dùng SQL Server Express, đổi `Server=localhost` thành `Server=localhost\\SQLEXPRESS` hoặc `Server=<TEN_MAY>\\SQLEXPRESS`.
  - Nếu đăng nhập bằng tài khoản SQL (`sa`), thay `Trusted_Connection=True` bằng `User Id=sa;Password=<mat_khau>;`.
- **Lưu ý Email SMTP:** Chức năng Quên mật khẩu sử dụng SMTP Gmail (`smtp.gmail.com:587`), cần sử dụng **Mật khẩu ứng dụng (App Password)** 16 ký tự của tài khoản Google.

### Bước 4: Cấu hình Google OAuth & MoMo Sandbox (Tùy chọn)

- **Google Login:** Chỉnh sửa `CLIENTID` và `REDIRECT` (`https://localhost:7186`) trong file `./wwwroot/Template/js/getAPI.js`.
- **MoMo Sandbox IPN:** Chỉnh sửa `ngrokUrl` (URL công khai trỏ tới `/payment/momo/savepay`) trong file `./Areas/Payment/Controllers/MoMoController.cs`.

---

## 4. Chạy ứng dụng

### Cách 1: Chạy trực tiếp bằng .NET 9 CLI

```bash
dotnet restore
dotnet build
dotnet run --launch-profile https
```

Sau khi khởi động thành công, truy cập trình duyệt tại:
- HTTPS: `https://localhost:7186`
- HTTP: `http://localhost:5180`

### Cách 2: Chạy bằng Docker

```bash
docker build -t dayhoctructuyen .
docker run -d -p 8080:8080 --name dayhoctructuyen-app dayhoctructuyen
```

Truy cập ứng dụng tại `http://localhost:8080` *(lưu ý cấu hình connection string trỏ tới SQL Server có thể truy cập từ container, ví dụ `host.docker.internal`)*.

---

## 5. Tài khoản test hệ thống (System Credentials)

Dữ liệu mẫu sau khi import `./wwwroot/Content/SQL/script.sql` bao gồm sẵn các tài khoản theo từng phân quyền để kiểm thử hệ thống:

| Vai trò | Mã ND | Email đăng nhập | Mật khẩu |
| :--- | :--- | :--- | :--- |
| **Admin** (`01`) | `U000001` | `trieukhanhbang123@gmail.com` | `123456` |
| **Giáo viên** (`02`) | `U000002` | `linhtuyet@gmail.com` | `123456` |
| **Giáo viên** (`02`) | `U000004` | `thuha@gmail.com` | `123456` |
| **Giáo viên** (`02`) | `U000006` | `anhnguyet@gmail.com` | `123456` |
| **Học sinh** (`03`) | `U000003` | `minhnguyet@gmail.com` | `123456` |
| **Học sinh** (`03`) | `U000005` | `nhatduy@gmail.com` | `123456` |
| **Học sinh** (`03`) | `U000008` | `quocthang@gmail.com` | `123456` |

---

## 6. Cấu trúc thư mục chính

```text
DayHocTrucTuyen/
├── Areas/
│   ├── Admin/         # Quản lý người dùng, phê duyệt giáo viên, kiểm duyệt lớp & rút tiền
│   ├── Courses/       # Quản lý lớp học, bài đăng, bình luận, thi trắc nghiệm, báo cáo
│   ├── Payment/       # Tích hợp cổng thanh toán MoMo
│   └── User/          # Ví người dùng, lịch sử giao dịch, chat SignalR, thông báo, email
├── Controllers/       # Trang chủ (Default), Đăng nhập/Đăng ký (Account), Hồ sơ (Profile), Phân quyền (Access)
├── Models/            # Entities (EF Core 9), ChatHub (SignalR), RolesAuthorizationHandler
├── Views/             # Razor Views & Shared Layouts
├── wwwroot/           # Static files, Content/SQL/script.sql, hình ảnh & tệp đính kèm
├── Dockerfile         # Cấu hình build Docker cho .NET 9
└── Program.cs         # Cấu hình DI, Authentication, SignalR & Middleware pipeline
```
