import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

import { environment } from '../../../environments/environment';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const accessToken = authService.getAccessToken();

  if (!accessToken || !isApiRequest(req.url)) {
    return next(req);
  }

  return next(
    req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    })
  );
};

function isApiRequest(url: string): boolean {
  if (url.startsWith('/api/') || url.startsWith('api/')) {
    return true;
  }

  if (url.startsWith(environment.apiBaseUrl)) {
    return true;
  }

  if (/^https?:\/\//i.test(url)) {
    try {
      return new URL(url).pathname.startsWith('/api/');
    } catch {
      return false;
    }
  }

  return false;
}
