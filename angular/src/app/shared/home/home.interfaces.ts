export interface Widget<T> {
  id: number | string;
  label: string;
  content: T,
  height: number;
  width: number;
}
