# 🇰🇷 Hệ Thống Quản Lý Chuỗi Nhà Hàng Hàn Quốc (Korean Restaurant Chain Management)

> **Tài liệu tổng quan tính năng & kiến trúc dự án (Bản nháp v1.0)**
> Tài liệu này tổng hợp toàn bộ các chức năng cốt lõi đã đề xuất cho hệ thống quản lý chuỗi nhà hàng Hàn Quốc, kèm theo các gợi ý mở rộng để dễ dàng rà soát và bổ sung thêm trước khi bước vào giai đoạn thiết kế chi tiết & lập trình.

---

## 🎯 Mục Tiêu Dự Án

Xây dựng một nền tảng **All-in-One** phục vụ đồng thời 3 nhóm đối tượng:
1. **Khách hàng:** Xem bảng tin (Newsfeed), khám phá các cơ sở, đặt bàn trực tuyến, xem menu.
2. **Nhân viên & Quản lý cơ sở:** Chấm công (Check-in/Check-out), xem/xếp lịch làm theo tuần, tính lương, chat & nhận thông báo realtime, quản lý menu & hóa đơn.
3. **Sếp / Chủ chuỗi (Super Admin):** Giám sát toàn bộ hệ thống chi nhánh, theo dõi doanh thu – chi phí – lợi nhuận tổng hợp, dùng AI đọc hóa đơn và xuất báo cáo Excel cuối tháng.

---

## 👥 Phân Quyền Hệ Thống (Roles)

| Vai trò | Phạm vi quyền hạn chính |
| :--- | :--- |
| **👑 Sếp / Chủ chuỗi (CEO / Super Admin)** | Xem toàn bộ cơ sở, dashboard doanh thu tổng & từng chi nhánh, duyệt báo cáo tài chính/Excel, quản lý quản lý cơ sở, xem tất cả dữ liệu. |
| **🧑‍💼 Quản lý cơ sở (Branch Manager)** | Quản lý nhân viên tại chi nhánh, xếp lịch làm theo tuần, tính lương, thêm/sửa/in menu, tạo hóa đơn, xác nhận đặt bàn, đăng bài lên Newsfeed. |
| **🧑‍🍳 Nhân viên (Staff / Bếp / Phục vụ / Thu ngân)** | Xem lịch làm cá nhân, check-in/check-out giờ làm, xem bảng công/lương cá nhân, chat realtime nội bộ, nhận thông báo realtime. |
| **🍽️ Khách hàng (Customer)** | Lướt Newsfeed nhà hàng, xem danh sách cơ sở, xem menu, đặt bàn trực tuyến, tương tác bài viết, nhận thông báo xác nhận bàn. |

---

## 🧩 Các Phân Hệ Chức Năng Chính

### 1. 🏢 Quản Lý Chuỗi Nhà Hàng & Tổng Quan Doanh Thu (Multi-Branch & Analytics)
- **Danh sách cơ sở:** Hiển thị đầy đủ các chi nhánh (tên cơ sở, địa chỉ, hotline, giờ mở cửa, trạng thái hoạt động, số bàn trống/đang phục vụ).
- **Dashboard cho Sếp:**
  - Xem tổng doanh thu toàn chuỗi theo ngày / tuần / tháng / quý / năm.
  - So sánh hiệu suất kinh doanh giữa các cơ sở (biểu đồ cột, đường, top cơ sở doanh thu cao nhất).
  - Theo dõi chi phí nhập hàng vs doanh thu bán ra để tính lợi nhuận thực tế.
  - Lọc và xem chi tiết từng cơ sở chỉ với 1 cú click.

---

### 2. 📱 Newsfeed Khách Hàng & Đặt Bàn Trực Tuyến (Social Feed & Booking)
- **Newsfeed (Giao diện tương tự Facebook):**
  - Hiển thị danh sách bài đăng từ chuỗi hoặc từng cơ sở: món mới, combo ưu đãi, sự kiện, không gian nhà hàng, review.
  - Khách hàng có thể **Thích (Like), Bình luận (Comment), Chia sẻ (Share)** hoặc lưu bài viết.
  - Bộ lọc bài viết theo cơ sở gần nhất hoặc theo chủ đề (Khuyến mãi, Thực đơn mới, Sự kiện).
- **Đặt bàn trực tuyến (Table Reservation):**
  - Chọn cơ sở muốn đến, ngày, khung giờ, số lượng khách (người lớn/trẻ em).
  - Chọn khu vực mong muốn (phòng VIP, bàn nướng BBQ, ngoài trời...) và ghi chú yêu cầu đặc biệt (sinh nhật, dị ứng món...).
  - Gửi thông báo realtime cho Quản lý/Lễ tân cơ sở để xác nhận; cập nhật trạng thái đặt bàn cho khách (Chờ duyệt → Đã xác nhận → Đã nhận bàn / Hủy).

---

### 3. 📅 Quản Lý Nhân Sự, Lịch Làm Việc & Tính Lương (HR, Scheduling & Payroll)
- **Xếp lịch làm việc theo tuần:**
  - Quản lý tạo & sắp xếp ca làm (Ca sáng, Ca chiều, Ca tối, Full-time/Part-time) cho từng nhân viên theo tuần.
  - Hỗ trợ sao chép lịch từ tuần trước, cảnh báo trùng ca hoặc thiếu người ở khung giờ cao điểm.
  - Cho phép nhân viên đăng ký ca rảnh hoặc xin đổi ca (cần quản lý duyệt).
- **Check-in / Check-out giờ làm:**
  - Nhân viên vào app/web để check-in khi bắt đầu ca và check-out khi kết thúc ca.
  - Đối chiếu thời gian check-in thực tế với lịch làm được phân công (tự động ghi nhận đi muộn, về sớm, tăng ca).
  - *(Tùy chọn)* Hỗ trợ xác thực check-in bằng mã QR tại cửa hàng hoặc định vị GPS/Wi-Fi nội bộ để tránh chấm công hộ.
- **Tính lương nhân viên:**
  - Tự động tổng hợp số giờ làm/số công chuẩn trong tháng.
  - Cấu hình lương theo giờ hoặc lương cứng + phụ cấp (ăn ca, gửi xe, trách nhiệm) + thưởng doanh số / tip + khấu trừ (đi muộn, ứng lương).
  - Xuất phiếu lương chi tiết cho từng nhân viên và xuất bảng lương tổng hợp ra file Excel.

---

### 4. 💬 Chat Realtime & Thông Báo Realtime (Realtime Communication)
- **Chat Realtime:**
  - Kênh chat nội bộ theo từng cơ sở, nhóm bộ phận (Bếp, Phục vụ, Thu ngân, Quản lý) và chat 1-1.
  - Hỗ trợ gửi tin nhắn văn bản, hình ảnh, tệp tin, tag tên (`@mention`) để trao đổi công việc nhanh chóng.
  - *(Mở rộng)* Kênh chat hỗ trợ trực tiếp giữa Khách hàng và Lễ tân/CSKH của cơ sở.
- **Thông báo Realtime (Push / In-app Notification):**
  - Báo ngay lập tức khi có: đơn đặt bàn mới, tin nhắn mới, lịch làm việc tuần mới được công bố, yêu cầu đổi ca được duyệt, thông báo thực đơn mới hoặc hóa đơn cần duyệt.

---

### 5. 📜 Quản Lý Thực Đơn & In Menu (Menu Management & Printing)
- **Thêm & điều chỉnh thực đơn:**
  - Thêm món mới (tên tiếng Việt / tiếng Hàn, hình ảnh, giá bán, mô tả, danh mục: Thịt nướng, Lẩu, Tokbokki, Đồ uống, Combo...).
  - Cập nhật giá, bật/tắt trạng thái **Còn món / Hết món** theo thời gian thực.
  - Áp dụng menu chung cho toàn chuỗi hoặc tùy chỉnh giá/món đặc trưng riêng cho từng cơ sở.
- **In Menu Mới:**
  - Tự động đổ dữ liệu thực đơn hiện tại vào các mẫu thiết kế (template) sẵn có.
  - Xuất file **PDF chất lượng cao** chuẩn khổ in (A4, A3, dạng cuốn/tờ rơi) để in ấn tại quầy ngay khi cập nhật món/giá mới.
  - *(Tùy chọn)* Tạo mã **QR Menu** cho từng bàn để khách quét xem thực đơn trực tuyến.

---

### 6. 🧾 Hóa Đơn Xuất/Mua, AI Đọc Hóa Đơn & Xuất Excel (Invoices, AI OCR & Reporting)
- **Quản lý hóa đơn xuất & mua:**
  - **Hóa đơn bán hàng (Xuất):** Ghi nhận doanh thu từng bàn/đơn hàng, phương thức thanh toán (Tiền mặt, Chuyển khoản, Thẻ, Ví điện tử), VAT/giảm giá.
  - **Hóa đơn nhập hàng / chi phí (Mua):** Ghi nhận hóa đơn nhập thịt, rau củ, gia vị Hàn Quốc, gas, điện nước, chi phí vận hành.
- **🤖 AI Đọc Hóa Đơn (AI OCR / Document Extraction):**
  - Cho phép chụp ảnh hoặc tải lên file PDF/ảnh hóa đơn giấy (từ nhà cung cấp hoặc hóa đơn bán lẻ).
  - AI tự động nhận diện và bóc tách dữ liệu có cấu trúc: **Ngày hóa đơn, Tên nhà cung cấp, Danh sách mặt hàng, Số lượng, Đơn vị tính, Đơn giá, Thành tiền, Thuế, Tổng cộng**.
  - Hiển thị dạng bảng để kế toán/quản lý kiểm tra nhanh và lưu vào hệ thống.
- **📊 Xuất File Excel & Tổng Hợp Cuối Tháng:**
  - Xuất dữ liệu các bảng hóa đơn (nhập/xuất) ra file `.xlsx` theo chuẩn kế toán.
  - Tự động tổng hợp báo cáo cuối tháng: Bảng kê hóa đơn đầu vào/đầu ra, Báo cáo doanh thu theo cơ sở, Báo cáo chi phí, Bảng lương nhân viên.

---

## 💡 Gợi Ý Các Chức Năng Có Thể Bổ Sung Thêm

Dưới đây là các tính năng thực tế thường rất cần cho chuỗi nhà hàng Hàn Quốc (đặc biệt mô hình BBQ / Lẩu) để bạn cân nhắc thêm vào dự án:

1. **🍽️ POS & Gọi món tại bàn (Order / QR Order):**
   - Nhân viên dùng tablet order tại bàn hoặc khách tự quét QR tại bàn để gọi thêm món (thịt, panchan, nước), bắn thẳng phiếu xuống Bếp/Bar.
2. **🖥️ Màn hình Bếp/Bar (KDS - Kitchen Display System):**
   - Hiển thị danh sách món cần làm theo thứ tự thời gian, đánh dấu đã lên món để tránh sót món khi đông khách.
3. **📦 Quản lý Kho & Định lượng nguyên liệu (Inventory):**
   - Theo dõi tồn kho thịt/gia vị tại từng cơ sở, tự động trừ kho khi bán món, cảnh báo nguyên liệu sắp hết hoặc sắp hết hạn.
4. **🎁 Khách hàng thân thiết (CRM & Membership):**
   - Tích điểm theo số điện thoại, phân hạng thành viên (Bạc, Vàng, VIP), tặng voucher sinh nhật, mã giảm giá áp dụng toàn chuỗi.
5. **🌐 Đa ngôn ngữ (Việt - Hàn - Anh):**
   - Hữu ích nếu sếp hoặc quản lý/đầu bếp là người Hàn Quốc, hoặc phục vụ khách du lịch/khách Hàn tại Việt Nam.
6. **🪑 Sơ đồ bàn trực quan (Floor Plan):**
   - Xem sơ đồ bàn theo thời gian thực: Bàn trống (Xanh), Đang ăn (Đỏ), Đã đặt trước (Vàng), Gộp bàn / Chuyển bàn.

---

## 🛠️ Định Hướng Công Nghệ Đề Xuất (Tham Khảo)

*(Có thể thay đổi tùy theo team phát triển và ngân sách)*

- **Frontend (Web & Admin):** React / Next.js + TailwindCSS (giao diện hiện đại, responsive).
- **Mobile App (Nhân viên / Khách hàng - tùy chọn):** React Native hoặc Flutter (hoặc dùng PWA trên trình duyệt).
- **Backend API:** Node.js (NestJS) hoặc Python (FastAPI) / Go.
- **Realtime (Chat & Thông báo):** Socket.io / WebSockets / Redis Pub-Sub.
- **Database:** PostgreSQL (lưu dữ liệu giao dịch, hóa đơn, nhân sự) + Redis (cache, realtime).
- **AI Đọc Hóa Đơn:** Gemini Vision API / OpenAI Vision / Google Document AI để bóc tách bảng từ ảnh hóa đơn.
- **Xuất Excel & In PDF Menu:** `ExcelJS` / `SheetJS` (xuất `.xlsx` nhiều sheet, có công thức tổng hợp) và `Puppeteer` / `React-PDF` (render mẫu in Menu chuẩn nét).

---

## 📝 Ghi Chú Rà Soát

- [ ] Chốt danh sách tính năng bắt buộc ở **Giai đoạn 1 (MVP)** và các tính năng làm ở **Giai đoạn 2**.
- [ ] Bổ sung thêm các nghiệp vụ đặc thù riêng của chuỗi nhà hàng (nếu có).
- [ ] Xác nhận quy trình tính lương & mẫu file Excel mong muốn.
