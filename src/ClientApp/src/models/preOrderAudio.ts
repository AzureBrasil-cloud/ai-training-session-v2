type AITransformedOrder = {
  extras: string[];
  size: number;
}

export interface PreOrderAudio {
  id: string;
  userEmail: string;
  audioName: string;
  audioExtension: string;
  content: string;
  createdAt: string;
  aiTransformedOrder: AITransformedOrder | null;
}
