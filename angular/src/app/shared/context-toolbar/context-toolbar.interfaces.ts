export interface ToolbarButton<T = any> {
  id: string;
  text: string;
  icon?: string;
  width?: number;
  class?: string;
  tooltip?: string;
  action: (context?: T) => void;
  disabled: boolean;
  visible: boolean;
}
