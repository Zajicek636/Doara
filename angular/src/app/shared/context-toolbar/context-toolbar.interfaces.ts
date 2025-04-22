export interface ToolbarButton<T = any> {
  id: string;
  text: string;
  icon?: string;
  class?: string;
  action: (context?: T) => void;
  disabled: boolean;
  visible: boolean;
}
