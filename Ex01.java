import javax.swing.*;
import java.awt.*;

public class EditorTexto {
    public static void main(String[] args) {
        // Criar a janela principal
        JFrame frame = new JFrame("Editor de Texto");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(800, 600);
        
        // Criar um painel principal com BorderLayout
        JPanel mainPanel = new JPanel(new BorderLayout());
        
        // Criar um painel para os botões (30% da largura)
        JPanel buttonPanel = new JPanel();
        buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.Y_AXIS));
        buttonPanel.setPreferredSize(new Dimension(frame.getWidth() * 30 / 100, frame.getHeight()));
        
        // Adicionar botões
        JButton abrirButton = new JButton("Abrir");
        JButton salvarButton = new JButton("Salvar");
        JButton salvarComoButton = new JButton("Salvar Como");
        JButton fecharButton = new JButton("Fechar");
        
        // Configurar tamanho dos botões
        Dimension buttonSize = new Dimension(150, 40);
        abrirButton.setPreferredSize(buttonSize);
        salvarButton.setPreferredSize(buttonSize);
        salvarComoButton.setPreferredSize(buttonSize);
        fecharButton.setPreferredSize(buttonSize);
        
        // Adicionar espaçamento entre os botões
        buttonPanel.add(Box.createVerticalStrut(10));
        buttonPanel.add(abrirButton);
        buttonPanel.add(Box.createVerticalStrut(10));
        buttonPanel.add(salvarButton);
        buttonPanel.add(Box.createVerticalStrut(10));
        buttonPanel.add(salvarComoButton);
        buttonPanel.add(Box.createVerticalStrut(10));
        buttonPanel.add(fecharButton);
        buttonPanel.add(Box.createVerticalGlue());
        
        // Criar a área de texto com barra de rolagem
        JTextArea textArea = new JTextArea();
        JScrollPane scrollPane = new JScrollPane(textArea);
        
        // Adicionar os componentes ao painel principal
        mainPanel.add(buttonPanel, BorderLayout.WEST);
        mainPanel.add(scrollPane, BorderLayout.CENTER);
        
        // Adicionar o painel principal à janela
        frame.add(mainPanel);
        
        // Centralizar a janela na tela
        frame.setLocationRelativeTo(null);
        
        // Tornar a janela visível
        frame.setVisible(true);
    }
}