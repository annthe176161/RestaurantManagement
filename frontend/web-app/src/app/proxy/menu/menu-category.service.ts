import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface MenuCategoryDto {
  id: string;
  nameVi: string;
  nameKo?: string;
  nameEn?: string;
  sortOrder: number;
  isActive: boolean;
}

@Injectable({ providedIn: 'root' })
export class MenuCategoryProxyService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/menu-categories`;

  getList(): Observable<MenuCategoryDto[]> {
    return this.http.get<MenuCategoryDto[]>(this.baseUrl);
  }
}
