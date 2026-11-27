import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzMessageService } from 'ng-zorro-antd/message';

export interface NewsfeedPostItem {
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
  selector: 'app-newsfeed-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    NzButtonModule,
    NzTagModule,
    NzInputModule,
    NzDividerModule
  ],
  templateUrl: './newsfeed-page.component.html',
  styleUrl: './newsfeed-page.component.scss'
})
export class NewsfeedPageComponent {
  searchText = '';
  selectedFilter = 'Tất cả';
  filters = ['Tất cả', 'Khuyến mãi', 'Món mới', 'Sự kiện'];
  visibleCount = 3;

  allPosts: NewsfeedPostItem[] = [
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
    },
    {
      id: 'p4',
      branchName: 'Seoul Garden - Cầu Giấy',
      authorName: 'Chi nhánh Cầu Giấy',
      publishedAt: '5 ngày trước',
      category: 'Khuyến mãi',
      title: 'Happy Lunch 11:00 - 14:00: Giảm 15% toàn bộ set Cơm trộn & Mì lạnh',
      content:
        'Bữa trưa văn phòng nhanh gọn, đủ chất với Cơm trộn thố đá Bibimbap, Miến trộn Japchae và Canh Kimchi đậu hũ non chuẩn vị Hàn.',
      likes: 64,
      comments: 9
    },
    {
      id: 'p5',
      branchName: 'Toàn chuỗi Seoul Garden',
      authorName: 'Seoul Garden Official',
      publishedAt: '1 tuần trước',
      category: 'Món mới',
      title: 'Thử ngay Tokbokki Phô Mai Đút Lò phiên bản cay nồng mới',
      content:
        'Sự kết hợp giữa bánh gạo tươi nhập khẩu, chả cá Busan, trứng luộc lòng đào và lớp phô mai Mozzarella phủ dày béo ngậy.',
      likes: 119,
      comments: 22
    }
  ];

  constructor(private readonly message: NzMessageService) {}

  get filteredPosts(): NewsfeedPostItem[] {
    const keyword = this.searchText.trim().toLowerCase();
    return this.allPosts.filter((post) => {
      const matchCategory = this.selectedFilter === 'Tất cả' || post.category === this.selectedFilter;
      const matchKeyword =
        !keyword ||
        post.title.toLowerCase().includes(keyword) ||
        post.content.toLowerCase().includes(keyword) ||
        post.branchName.toLowerCase().includes(keyword);
      return matchCategory && matchKeyword;
    });
  }

  get visiblePosts(): NewsfeedPostItem[] {
    return this.filteredPosts.slice(0, this.visibleCount);
  }

  get hasMorePosts(): boolean {
    return this.visibleCount < this.filteredPosts.length;
  }

  selectFilter(filter: string): void {
    this.selectedFilter = filter;
    this.visibleCount = 3;
  }

  loadMore(): void {
    this.visibleCount += 3;
  }

  toggleLike(post: NewsfeedPostItem): void {
    post.likedByMe = !post.likedByMe;
    post.likes += post.likedByMe ? 1 : -1;
  }

  promptLogin(action: string): void {
    this.message.info(`Vui lòng đăng nhập để ${action}.`);
  }
}
