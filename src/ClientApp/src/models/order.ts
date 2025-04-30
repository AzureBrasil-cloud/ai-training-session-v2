export type Order = {
  id: string;
  userEmail: string;
  size: number;
  extras: string[];
  totalValue: number | null;
  createdAt: Date | null;
}
