export type BannerGet = {
    slideId: number;
    title: string;
    link: string;
    imageUrl: string;
    status: boolean;
    description: string;
  };
  
  export type BannerPost = {
    title: string;
    link: string;
    imageFile: File;
    description: string;
    status: boolean;
};
