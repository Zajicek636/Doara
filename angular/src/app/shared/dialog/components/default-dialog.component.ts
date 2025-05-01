export abstract class DefaultDialogComponent<T = any, R = any> {
  data!: T;
  close!: (result?: R) => void;
}
