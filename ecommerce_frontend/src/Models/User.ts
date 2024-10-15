export type UserProfileToken = {
    customerId: number
    role: string;
    email: string;
    name: string;
    token: string;
  };

  export type UserProfile = {
    customerId: number
    role: string;
    email: string;
    name: string;
  };

  export type Customer = {
    customerId: number,
    name: string,
    email: string,
    phone: string,
    address: string,
    imageUrl: string,
}