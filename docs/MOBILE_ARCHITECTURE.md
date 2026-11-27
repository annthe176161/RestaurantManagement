# Kiến Trúc Mobile App - Flutter + Dart

Tài liệu mô tả cấu trúc thư mục Mobile (`mobile/mobile-app/`) sử dụng **Flutter + Dart** theo mô hình **Feature-First Clean Architecture**, kết nối tới Backend thông qua `ApiGateway`.

---

## 1. Cấu Trúc Thư Mục `mobile/mobile-app/`

```text
mobile/mobile-app/
├── lib/
│   ├── core/                        # Hạ tầng dùng chung toàn ứng dụng
│   │   ├── config/                  # Cấu hình API Gateway URL, môi trường (dev/prod)
│   │   ├── network/                 # HTTP Client (Dio), Auth/Refresh Token Interceptor
│   │   ├── storage/                 # SecureStorage (lưu JWT), Local Cache
│   │   ├── realtime/                # SignalR Client (Chat & Thông báo realtime)
│   │   ├── router/                  # Điều hướng (GoRouter) & phân quyền theo Role
│   │   ├── theme/                   # Giao diện, màu sắc, typography
│   │   ├── localization/            # Đa ngôn ngữ (vi, ko, en)
│   │   └── utils/                   # Format tiền tệ/ngày giờ, Validators, Camera/QR helpers
│   │
│   ├── shared/                      # Thành phần UI & Models tái sử dụng
│   │   ├── widgets/                 # Common Widgets (Buttons, Loading, BottomNav theo Role)
│   │   └── models/                  # Shared Models (User, Role, Branch, PagedResult)
│   │
│   └── features/                    # Các phân hệ nghiệp vụ (mỗi feature gồm data / domain / presentation)
│       ├── auth/                    # Đăng nhập, đăng ký, phân luồng theo vai trò
│       ├── newsfeed/                # Bảng tin (Facebook-style) cho Khách hàng & Quản lý
│       ├── branches/                # Xem danh sách cơ sở & dashboard doanh thu nhanh (Sếp)
│       ├── table_booking/           # Đặt bàn trực tuyến & xác nhận đặt bàn
│       ├── menu/                    # Xem thực đơn, QR Menu, cập nhật trạng thái còn/hết món
│       ├── orders/                  # Gọi món tại bàn (Nhân viên ngoài) & theo dõi món (Bếp)
│       ├── attendance_schedule/     # Xem lịch làm tuần, Check-in/Check-out (QR/GPS), xem lương
│       ├── ai_ocr_invoice/          # Chụp ảnh hóa đơn giấy gửi AiOcrService bóc tách dữ liệu
│       ├── chat_notifications/      # Chat realtime nội bộ/CSKH & Trung tâm thông báo
│       └── profile_settings/        # Thông tin cá nhân, đổi mật khẩu, đổi ngôn ngữ
│
├── assets/
│   ├── images/
│   └── translations/                # File dịch vi.json, ko.json, en.json
│
└── test/                            # Unit tests & Widget tests
```

---

## 2. Chức Năng Chính Theo Vai Trò Trên Mobile

| Vai trò | Trải nghiệm chính trên Mobile |
| :--- | :--- |
| **Sếp** | Xem nhanh doanh thu tổng chuỗi & từng cơ sở, duyệt báo cáo, nhận thông báo quan trọng, chat nội bộ. |
| **Quản lý** | Xác nhận đặt bàn, duyệt đổi ca, chụp ảnh hóa đơn gửi AI OCR, đăng bài Newsfeed, chat & thông báo realtime. |
| **Nhân viên ngoài** | Xem lịch làm theo tuần, check-in/check-out vào ca, xem bảng lương, gọi món tại bàn (tablet/mobile), chat nội bộ. |
| **Bếp** | Xem lịch làm theo tuần, check-in/check-out vào ca, xem bảng lương, nhận đơn món / báo hết món, chat với nhân viên ngoài & quản lý. |
| **Khách hàng** | Lướt Newsfeed, xem các cơ sở, xem Menu, đặt bàn trực tuyến, nhận thông báo xác nhận bàn. |
