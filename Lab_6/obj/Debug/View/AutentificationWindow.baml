   M S B A M L   `   `   `  ????@  <Lab_6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_6Lab_6  5   	   !    Lab_6.AutentificationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Lab_6     5   	   -   5          ?Name$  	autWindow??5   	    ? 
ResizeMode$ NoResize=?5	   	      AutentificationTitle ? Title# ?   5
   	   $	??326??68   $	??367??6E    ? Icon$, &/View/icons8-строитель-96.png??6Q    J? InputBindings 5      ?? 5   
   
 ?? Key$ Return??6    ?? Command 5      ?? *	LogInUser+ ?? CommandParameter 5      ??  ?? RelativeSource ?? 	 ?? Mode$	 FindAncestor=?
 ?? AncestorType"
 ?.??5      ?  ? RowDefinitions 5   
   ?? 5      $	??14* ?6   ?? 5      $	??95* ?6   ?? 5      $
??201* ?6    ? ColumnDefinitions 5   
   ?? 5      $	??62* ?6   ?? 5      $	??65* ?6   ?? 5      $
??178* ?6   ?? 5      $	??62* ?6   .+?5   
   ??  ?? Orientation$ Vertical=?6   $??3??6,   $??0,14,2,0q?6=   $??1??6O   $??2??6_   .+?5      ?? $??16(?6     UserName#???  6&   ?? 5      -   5         ??Name$ LoginTextBox??5      ??6,   ?? *User.LOGIN_USER+ ?? Mode$ TwoWay=?6\   ?? 5       $??16(?6     UserPassword#???  6&   ?? 5!      -   5        $ PasswordTextBox??5!      ??6/   ?? *User.PASSWORD_USER+$ TwoWay=?6b   5$      ?? 5%   
   $ Vertical=?6   $??2??6,   $??4??69   $??Bottom=?6M   $
??Auto??6h   .+?5&      ?? $??30??6   $	??130??6"   $??3q?6.    	 LogIn#???  69   ??6[   ?? *	LogInUser+??6y   ??  ?? $	 FindAncestor=?"
 ??? 5'      $??30??6   $	??130??6!   $??3q?6-     Register#???  68   ??6]   ?? *RegisterUser+??6~   ??  ?? $	 FindAncestor=?"
 ??? 5(      -   5         ??Name$ LoginGuestBtn??5(      $??20??6,     EnterAsGuest#???  68   ??6a   ?? *
LoginGuest+??6?   ??  ?? $	 FindAncestor=?"
 ?5)      5*      5+      