GO
Create database DayHocTrucTuyen
GO
use DayHocTrucTuyen
GO

create table LoaiND(
	Ma_Loai char(2) primary key,
	Ten_Loai nvarchar(10) not null
)

create table NguoiDung(
	Ma_ND char(7) primary key,
	Ma_Loai char(2) references LoaiND(Ma_Loai),
	Ho_Lot nvarchar(20),
	Ten nvarchar(7) not null,
	Ngay_Sinh datetime,
	Gioi_Tinh int,
	SDT varchar(11),
	Email varchar(50) not null unique,
	Mat_Khau varchar(32) not null,
	Img_Avt varchar(100),
	Img_BG varchar(100),
	Trang_Thai bit not null,
	Mo_Ta nvarchar(300),
	Ngay_Tao datetime not null,
	Bi_Danh varchar(20) unique
)

create table PheDuyet(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	Ngay_Dang_Ky datetime not null,
	Trang_Thai bit not null,
	Ghi_Chu nvarchar(200)
)

create table ViNguoiDung(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	So_Du float not null,
	Ngay_Mo datetime not null,
	Trang_Thai bit not null
)

create table GoiNangCap(
	Ma_Goi int primary key,
	Ten_Goi nvarchar(10) not null,
	Gia_Tien float not null,
	Hieu_Luc int not null,
	Mo_Ta nvarchar(100)
)

create table TrangThaiNangCap(
	Ma_ND char(7) primary key references NguoiDung(Ma_ND),
	Ma_Goi int references GoiNangCap(Ma_Goi),
	Ngay_Dang_Ky datetime not null
)

create table LichSuGiaoDich(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime,
	Thu_Vao bit not null,
	So_Tien float not null,
	So_Du float not null,
	Ghi_Chu nvarchar(200) not null,
	primary key (Ma_ND, Thoi_Gian)
)

create table YeuCauThanhToan(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Loai_Thanh_Toan varchar(20) not null,
	So_Tai_Khoan varchar(20),
	So_Tien float not null,
	Trang_Thai int not null,
	Thoi_Gian datetime,
	Ghi_Chu nvarchar(200),
	primary key (Ma_ND, Thoi_Gian)
)

create table TinNhan(
	ID int primary key,
	Nguoi_Gui char(7) references NguoiDung(Ma_ND),
	Nguoi_Nhan char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500) not null,
	Trang_Thai bit not null
)

create table ThichTrang(
	Ma_YT char(15) primary key,
	Nguoi_Dung char(7) references NguoiDung(Ma_ND),
	Nguoi_Thich char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null
)

create table XemTrang(
	Ma_XT char(15) primary key,
	Nguoi_Dung char(7) references NguoiDung(Ma_ND),
	Nguoi_Xem char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null
)

create table BaoCao(
	Ma_Bao_Cao char(10) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Chi_Muc varchar(15) not null,
	Noi_Dung nvarchar(150) not null,
	Ghi_Chu nvarchar(500),
	Thoi_Gian datetime not null
)

create table ThongBao(
	Ma_TB char(5),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Loai_TB varchar(10) not null,
	Tieu_De nvarchar(20) not null,
	Noi_Dung nvarchar(500),
	Thoi_Gian datetime not null,
	Trang_Thai bit not null,
	Lien_Ket varchar(100),
	primary key (Ma_TB, Ma_ND)
)

create table LopHoc(
	Ma_Lop char(11) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ngay_Tao datetime not null,
	Ten_Lop nvarchar(40) not null,
	Bi_Danh varchar(20) unique,
	Gia_Tien float not null,
	Mo_Ta nvarchar(300),
	Trang_Thai bit not null,
	Img_Bia varchar(20)
)

create table Tag(
	Ma_Tag char(5) primary key,
	Ten_Tag nvarchar(30) not null
)

create table LopThuocTag(
	Id int primary key identity(1,1),
	Ma_Tag char(5) references Tag(Ma_Tag),
	Ma_Lop char(11) references LopHoc(Ma_Lop)
)

create table DanhGiaLop(
	Ma_DG char(15) primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Muc_Do int not null,
	Ghi_Chu nvarchar(500),
	Thoi_Gian datetime
)

create table LichSuTruyCap(
	Thoi_Gian datetime primary key,
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop)
)

create table HocSinhThuocLop(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ngay_Tham_Gia Datetime not null,
	primary key (Ma_ND, Ma_Lop)
)

create table BaiDang(
	Ma_Bai char(15) primary key,
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500),
	Dinh_Kem varchar(1000),
	Trang_Thai bit not null
)

create table Ghim(
	Ma_Bai char(15) primary key references BaiDang(Ma_Bai),
	Thoi_Gian datetime not null
)

create table BinhLuan(
	Ma_Bai char(15) references BaiDang(Ma_Bai),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Thoi_Gian datetime not null,
	Noi_Dung nvarchar(500),
	Dinh_Kem varchar(1000),
	primary key (Ma_Bai, Ma_ND, Thoi_Gian)
)

create table CamXuc(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Bai char(15) references BaiDang(Ma_Bai),
	Thoi_Gian datetime not null,
	primary key (Ma_ND, Ma_Bai)
)

create table PhongThi(
	Ma_Phong char(15) primary key,
	Ma_Lop char(11) references LopHoc(Ma_Lop),
	Ten_Phong nvarchar(50) not null,
	Ngay_Tao datetime not null,
	Mat_Khau varchar(50),
	Ngay_Mo datetime not null,
	Ngay_Dong datetime not null,
	Luot_Thi int not null,
	Xem_Lai bit not null,
	Thoi_Luong int not null
)

create table BiCamThi(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Ly_Do nvarchar(100),
	primary key (Ma_ND, Ma_Phong)
)

create table CauHoiThi(
	STT int,
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Cau_Hoi nvarchar(500) not null,
	Loi_Giai nvarchar(100) not null,
	Dap_An nvarchar(500) not null,
	primary key (STT, Ma_Phong)
)

create table CauTraLoi(
	STT int,
	Ma_Phong char(15),
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Lan_Thu int,
	Dap_An nvarchar(500) not null,
	primary key (STT, Ma_Phong, Ma_ND, Lan_Thu)
)
ALTER TABLE CauTraLoi ADD FOREIGN KEY (STT, Ma_Phong) REFERENCES CauHoiThi(STT, Ma_Phong)

create table ThoiGianLamBai(
	Ma_ND char(7) references NguoiDung(Ma_ND),
	Ma_Phong char(15) references PhongThi(Ma_Phong),
	Lan_Thu int not null,
	Bat_Dau datetime not null,
	Ket_Thuc datetime,
	primary key (Ma_ND, Ma_Phong, Lan_Thu)
)

GO