import { ChangeEventHandler, ReactElement, useState } from 'react'
import { TextInput, createStyles, PasswordInput } from '@mantine/core'

const useStyles = createStyles((theme, { floating, hasIcon }: { floating: boolean, hasIcon: boolean }) => ({
  root: {
    position: 'relative',
    width: '100%',
  },

  label: {
    position: 'absolute',
    zIndex: 2,
    top: 7,
    left: !floating && hasIcon ? 36 : theme.spacing.sm,
    pointerEvents: 'none',
    color: floating
      ? theme.colorScheme === 'dark'
        ? theme.white
        : theme.black
      : theme.colorScheme === 'dark'
        ? theme.colors.dark[3]
        : theme.colors.gray[5],
    transition: 'transform 150ms ease, color 150ms ease, font-size 150ms ease, left 150ms ease',
    transform: floating ? `translate(-${theme.spacing.sm}px, -28px)` : 'none',
    fontSize: floating ? theme.fontSizes.xs : theme.fontSizes.sm,
    fontWeight: floating ? 500 : 400,
  },

  required: {
    transition: 'opacity 150ms ease',
    opacity: floating ? 1 : 0,
  },

  input: {
    '&::placeholder': {
      transition: 'color 150ms ease',
      color: !floating ? 'transparent' : undefined,
    },
  },
}));

interface DFloatingLabelInputProps {
  label: string
  floating?: boolean
  required?: boolean
  value?: string
  onChange?: ChangeEventHandler<HTMLInputElement>
  onBlur?: () => void
  onFocus?: () => void
  placeholder?: string
  disabled?: boolean
  icon: ReactElement
}

export function DPasswordInput(props: DFloatingLabelInputProps) {
  const [focused, setFocused] = useState(false);
  const { classes } = useStyles({ floating: props.value?.trim().length !== 0 || focused, hasIcon: !!props.icon });

  return (
    <PasswordInput
      label={props.label}
      placeholder={props.placeholder}
      required
      classNames={classes}
      value={props.value}
      onChange={props.onChange}
      onFocus={() => setFocused(true)}
      onBlur={() => setFocused(false)}
      mt="md"
      icon={props.icon}
      autoComplete="nope"
    />
  );
}
