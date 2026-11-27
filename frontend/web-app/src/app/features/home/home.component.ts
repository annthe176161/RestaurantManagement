import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { BranchDto, BranchProxyService } from '../../proxy/branch/branch.service';
import { MenuCategoryDto, MenuCategoryProxyService } from '../../proxy/menu/menu-category.service';

interface FeaturedDish {
  id: string;
  categoryName: string;
  nameVi: string;
  nameKo: string;
  description: string;
  price: number;
  unit: string;
  isSignature?: boolean;
}

interface NewsfeedPost {
  id: string;
  branchName: string;
  authorName: string;
  publishedAt: string;
  category: 'Khuyến mãi' | 'Món mới' | 'Sự kiện';
  title: string;
  content: string;
  likes: number;
  comments: number;
  likedByMe?: boolean;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzButtonModule,
    NzCardModule,
    NzTagModule,
    NzInputModule,
    NzSelectModule,
    NzDatePickerModule,
    NzInputNumberModule,
    NzDividerModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private readonly branchService = inject(BranchProxyService);
  private readonly menuCategoryService = inject(MenuCategoryProxyService);
  private readonly message = inject(NzMessageService);

  branches: BranchDto[] = [];
  isUsingSampleBranches = false;

  categories: MenuCategoryDto[] = [];
  selectedMenuCategory = 'Tất cả';

  selectedFeedFilter = 'Tất cả';
  feedFilters = ['Tất cả', 'Khuyến mãi', 'Món mới', 'Sự kiện'];

  bookingForm = {
    branchName: '',
    date: null as Date | null,
    timeSlot: '19:00',
    guestCount: 4,
    area: 'Khu nướng BBQ',
    customerName: '',
    customerPhone: '',
    note: ''
  };

  timeSlots = ['11:00', '12:00', '13:00', '17:30', '18:30', '19:00', '19:30', '20:30'];
  areaOptions = ['Khu nướng BBQ', 'Phòng VIP', 'Khu gia đình', 'Ngoài trời'];

  private readonly sampleBranches: BranchDto[] = [
    {
      id: 'sample-1',
      code: 'SG-HK',
      name: 'Seoul Garden - Hoàn Kiếm',
      address: '28 Lý Thường Kiệt, Quận Hoàn Kiếm, Hà Nội',
      phone: '024 3939 6868',
      openTime: '10:00:00',
      closeTime: '22:30:00',
      totalTables: 32,
      status: 1
    },
    {
      id: 'sample-2',
      code: 'SG-CG',
      name: 'Seoul Garden - Cầu Giấy',
      address: '105 Trần Thái Tông, Quận Cầu Giấy, Hà Nội',
      phone: '024 3795 8686',
      openTime: '10:00:00',
      closeTime: '22:30:00',
      totalTables: 40,
      status: 1
    },
    {
      id: 'sample-3',
      code: 'SG-TD',
      name: 'Seoul Garden - Thảo Điền',
      address: '62 Xuân Thủy, Thảo Điền, TP. Thủ Đức, TP.HCM',
      phone: '028 3898 6868',
      openTime: '10:30:00',
      closeTime: '23:00:00',
      totalTables: 36,
      status: 1
    }
  ];

  dishes: FeaturedDish[] = [
    {
      id: 'd1',
      categoryName: 'Thịt nướng BBQ',
      nameVi: 'Ba chỉ heo nướng Samgyeopsal',
      nameKo: '삼겹살 구이',
      description: 'Ba chỉ heo tươi cắt dày chuẩn Hàn, nướng trên than hoa ăn kèm lá vừng và sốt Ssamjang.',
      price: 219000,
      unit: 'Phần 200g',
      isSignature: true
    },
    {
      id: 'd2',
      categoryName: 'Thịt nướng BBQ',
      nameVi: 'Sườn bò Mỹ tẩm ướp Galbi',
      nameKo: '양념 소갈비',
      description: 'Sườn bò Mỹ thượng hạng ướp nước lê Hàn Quốc và xì dầu đặc biệt trong 24 giờ.',
      price: 389000,
      unit: 'Phần 250g',
      isSignature: true
    },
    {
      id: 'd3',
      categoryName: 'Lẩu Hàn Quốc',
      nameVi: 'Lẩu Kimchi hải sản cay nồng',
      nameKo: '해물 김치전골',
      description: 'Nước dùng kimchi ủ lâu năm đậm đà, kết hợp mực, tôm, đậu hũ non và mì udon.',
      price: 349000,
      unit: 'Nồi 2-4 người'
    },
    {
      id: 'd4',
      categoryName: 'Món nóng & Đường phố',
      nameVi: 'Tokbokki phô mai kéo sợi',
      nameKo: '치즈 떡볶이',
      description: 'Bánh gạo dẻo dai hòa quyện sốt Gochujang cay ngọt và phô mai Mozzarella béo ngậy.',
      price: 129000,
      unit: 'Đĩa'
    },
    {
      id: 'd5',
      categoryName: 'Cơm & Mì',
      nameVi: 'Cơm trộn thố đá Bibimbap',
      nameKo: '돌솥 비빔밥',
      description: 'Cơm dẻo giòn cháy cạnh trong thố đá nóng, bò băm, 6 loại rau củ và trứng lòng đào.',
      price: 145000,
      unit: 'Thố'
    },
    {
      id: 'd6',
      categoryName: 'Món nóng & Đường phố',
      nameVi: 'Miến trộn bò nấm Japchae',
      nameKo: '소고기 잡채',
      description: 'Miến khoai lang Hàn Quốc dai ngon xào cùng thịt bò, nấm đông cô và dầu mè thơm lừng.',
      price: 139000,
      unit: 'Đĩa'
    }
  ];

  posts: NewsfeedPost[] = [
    {
      id: 'p1',
      branchName: 'Toàn chuỗi Seoul Garden',
      authorName: 'Seoul Garden Official',
      publishedAt: '2 giờ trước',
      category: 'Khuyến mãi',
      title: 'Ưu đãi nhóm 4 người - Tặng ngay 1 phần Ba chỉ bò Mỹ cuộn nấm',
      content:
        'Từ thứ 2 đến thứ 5 hàng tuần, khách hàng đặt bàn trước qua website cho nhóm từ 4 khách trở lên sẽ được tặng ngay 1 phần Ba chỉ bò Mỹ tươi kèm set Panchan 8 món miễn phí refill.',
      likes: 142,
      comments: 28
    },
    {
      id: 'p2',
      branchName: 'Seoul Garden - Hoàn Kiếm',
      authorName: 'Chi nhánh Hoàn Kiếm',
      publishedAt: 'Hôm qua lúc 18:30',
      category: 'Món mới',
      title: 'Ra mắt Menu Mùa Đông: Lẩu Bulgogi Nấm Rừng & Sườn Bò Hầm Kimchi',
      content:
        'Thực đơn mới đã chính thức có mặt tại tất cả các cơ sở. Nước dùng ngọt thanh từ xương bò ninh 12 tiếng kết hợp nấm hương tươi và thịt bò mềm tan.',
      likes: 98,
      comments: 15
    },
    {
      id: 'p3',
      branchName: 'Seoul Garden - Thảo Điền',
      authorName: 'Chi nhánh Thảo Điền',
      publishedAt: '3 ngày trước',
      category: 'Sự kiện',
      title: 'Không gian phòng VIP riêng tư cho tiệc sinh nhật & liên hoan công ty',
      content:
        'Hệ thống 6 phòng VIP sức chứa từ 8 đến 24 khách với hệ thống hút khói âm bàn hiện đại, hỗ trợ trang trí backdrop sinh nhật miễn phí khi đặt trước 24 giờ.',
      likes: 76,
      comments: 11
    }
  ];

  ngOnInit(): void {
    this.loadBranches();
    this.loadMenuCategories();
  }

  private loadBranches(): void {
    this.branchService.getList().subscribe({
      next: (res) => {
        if (res.items && res.items.length > 0) {
          this.branches = res.items;
          this.isUsingSampleBranches = false;
        } else {
          this.branches = this.sampleBranches;
          this.isUsingSampleBranches = true;
        }
        if (this.branches.length > 0) {
          this.bookingForm.branchName = this.branches[0].name;
        }
      },
      error: () => {
        this.branches = this.sampleBranches;
        this.isUsingSampleBranches = true;
        this.bookingForm.branchName = this.branches[0].name;
      }
    });
  }

  private loadMenuCategories(): void {
    this.menuCategoryService.getList().subscribe({
      next: (items) => {
        this.categories = items ?? [];
      },
      error: () => {
        this.categories = [];
      }
    });
  }

  get menuCategoryTabs(): string[] {
    if (this.categories.length > 0) {
      return ['Tất cả', ...this.categories.map((c) => c.nameVi)];
    }
    return ['Tất cả', 'Thịt nướng BBQ', 'Lẩu Hàn Quốc', 'Món nóng & Đường phố', 'Cơm & Mì'];
  }

  get filteredDishes(): FeaturedDish[] {
    if (this.selectedMenuCategory === 'Tất cả') {
      return this.dishes;
    }
    return this.dishes.filter((d) => d.categoryName === this.selectedMenuCategory);
  }

  get filteredPosts(): NewsfeedPost[] {
    if (this.selectedFeedFilter === 'Tất cả') {
      return this.posts;
    }
    return this.posts.filter((p) => p.category === this.selectedFeedFilter);
  }

  selectBranchAndBook(branchName: string): void {
    this.bookingForm.branchName = branchName;
    this.scrollTo('booking');
  }

  toggleLike(post: NewsfeedPost): void {
    post.likedByMe = !post.likedByMe;
    post.likes += post.likedByMe ? 1 : -1;
  }

  promptLoginForAction(actionName: string): void {
    this.message.info(`Vui lòng đăng nhập để ${actionName}.`);
  }

  submitBooking(): void {
    if (!this.bookingForm.customerName.trim() || !this.bookingForm.customerPhone.trim()) {
      this.message.warning('Vui lòng nhập họ tên và số điện thoại liên hệ.');
      return;
    }
    this.message.success(
      `Đã ghi nhận yêu cầu đặt bàn tại ${this.bookingForm.branchName} cho ${this.bookingForm.guestCount} khách lúc ${this.bookingForm.timeSlot}!`
    );
    this.bookingForm.customerName = '';
    this.bookingForm.customerPhone = '';
    this.bookingForm.note = '';
  }

  scrollTo(sectionId: string): void {
    const el = document.getElementById(sectionId);
    if (el) {
      el.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
