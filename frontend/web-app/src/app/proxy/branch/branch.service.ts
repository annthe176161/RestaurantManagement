import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface BranchDto {
  id: string;
  code: string;
  name: string;
  address: string;
  phone: string;
  email?: string;
  openTime: string;
  closeTime: string;
  totalTables: number;
  status: number;
}

export interface PagedResultDto<T> {
  totalCount: number;
  items: T[];
}

@Injectable({ providedIn: 'root' })
export class BranchProxyService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/api/branches`;

  getList(skipCount = 0, maxResultCount = 20): Observable<PagedResultDto<BranchDto>> {
    return this.http.get<PagedResultDto<BranchDto>>(this.baseUrl, {
      params: { skipCount, maxResultCount }
    });
  }
}
