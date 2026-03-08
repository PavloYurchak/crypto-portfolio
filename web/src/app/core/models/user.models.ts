export interface UserProfile {
  id: number;
  email: string;
  userName: string;
  emailConfirmed: boolean;
  isLockedOut: boolean;
  lockoutEndAt: string | null;
  failedAccessCount: number;
  twoFactorEnabled: boolean;
  userType: string;
  isActive: boolean;
  createdAt: string;
  updatedAt: string | null;
}
