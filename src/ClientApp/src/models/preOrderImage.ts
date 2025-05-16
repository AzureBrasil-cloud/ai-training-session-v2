type AITransformedOrder = {
  extras: string[];
  size: number;
}

export interface PreOrderImage {
  id: string;
  userEmail: string;
  imageName: string;
  imageExtension: string;
  keyValuePairs: string[];
  createdAt: string;
  aiTransformedOrder: AITransformedOrder | null;
}
